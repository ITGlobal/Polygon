using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.Connector.MicexBridge.Feed
{
    /// <summary>
    /// Класс реализует фид данных, работающий на шлюзе ММВБ.
    /// </summary>
    internal sealed class MicexBridgeFeed : Connector.Feed, IOrderBookSubscriber, IInstrumentParamsSubscriber, IFortsDataProvider, IInstrumentTickerLookup
    {
        /// <summary>
        /// Объект синхронизации.
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// Адаптер к данным для конкретной секции ММВБ.
        /// </summary>
        private MicexSectionFeedAdapter sectionFeedAdapter;

        /// <summary>
        /// Задача, в которой происходит обновление данных, периодический опрос шлюза.
        /// </summary>
        private readonly Task dataRefreshementTask;
        
        /// <summary>
        /// Логин с которым происходит подключение к шлюзу.
        /// </summary>
        private readonly string login;

        /// <summary>
        /// Набор таблиц доступных в данном интерфейсе шлюза.
        /// </summary>
        private TableType[] tableTypes;

        /// <summary>
        /// Индекс таблицы параметров инструментов.
        /// </summary>
        private int infoTableIdx;

        /// <summary>
        /// Индекс таблицы всех сделок.
        /// </summary>
        private int dealTableIdx;

        /// <summary>
        /// Индекс таблицы фондовых индексов.
        /// </summary>
        private int stockIndexesTableIdx;

        /// <summary>
        /// Индекс таблицы стаканов.
        /// </summary>
        private int quoteTableIdx;

        /// <summary>
        /// Тип секции ММВБ.
        /// </summary>
        private readonly MicexSecionType section;

        private readonly uint refreshTimeout;

        private readonly CancellationTokenSource cancelSource = new CancellationTokenSource();

        private readonly CancellationToken token;

        private readonly MicexBridgeConnector _connector;

        /// <summary>
        /// Мэп инструментов на параметры инструмента.
        /// </summary>
        private readonly Dictionary<Instrument, InstrumentParams> _instrumentsParams;

        /// <summary>
        /// Мэп инструментов на стаканы.
        /// </summary>
        private Dictionary<Instrument, OrderBook> _orderBooks;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sectionType">Секция рынка.</param>
        /// <param name="connectionString">Строка подключения.</param>
        /// <param name="login">Логин на сервер биржи.</param>
        /// <param name="dataRefreshTimeout">Частота обновления данных.</param>
        public MicexBridgeFeed(MicexSecionType sectionType, MicexBridgeConnector connector)
        {
            section = sectionType;
            refreshTimeout = 10;
            _connector = connector;
            token = cancelSource.Token;
            _instrumentsParams = new Dictionary<Instrument, InstrumentParams>();
            _orderBooks = new Dictionary<Instrument, OrderBook>();
            dataRefreshementTask = new Task(CycleProc, token);
        }


        #region Overrides of GatewayService

        /// <summary>
        /// Запускает фид данных.
        /// </summary>
        public override void Start()
        {
            try
            {
                Logger.Info().Print("Запускаем MicexFeed");
                
                if (_connector.ApiWrapper == null)
                {
                    Logger.Error().Print(null, "MicexFeed не запущен. Не удалось подключиться к mtesrl.dll.");
                    return;
                }

                Logger.Info().Print("Подключение к mtesrl.dll установлено.");

                lock (_connector.ApiWrapper)
                {
                    var info = _connector.ApiWrapper.GetServInfo();
                    Logger.Info().Print($"Информация о сервере: {info}");

                    MTETable infoMteTable;
                    TransactionType[] transactionsType;

                    // получаем структуру таблиц текущего интерфейса шлюза
                    _connector.ApiWrapper.Structure(out tableTypes, out transactionsType);

                    // создаём адаптер, соответствующий секции ММВБ
                    sectionFeedAdapter = MicexSectionFeedAdapter.CreateAdapter(section, tableTypes);

                    #region Открываем таблицу параметров и сохраняем все инструменты в контэйнере InstrumentsParams

                    // TODO Нужно индексы колонок взять на основе имён колонок, сейчас эти индексы захардкожены. 
                    var infoTableType  = tableTypes[sectionFeedAdapter.InfoTableIndex];
                    infoTableType.Input[0].Name

                    infoTableIdx = _connector.ApiWrapper.OpenTable(
                        tableTypes[sectionFeedAdapter.InfoTableIndex],
                        sectionFeedAdapter.InfoTableParams,
                        false,
                        out infoMteTable);

                    foreach (var row in infoMteTable.Rows)
                    {
                        var code = sectionFeedAdapter.GetInstrumentCodeFromParams(row);
                        // TODO Нужно ли куда-то присунуть classCode?
                        var classCode = sectionFeedAdapter.ClassCode(row);
                        var instrument = new Instrument
                        {
                            Code = code,
                        };

                        if (!_instrumentsParams.ContainsKey(instrument))
                            _instrumentsParams.Add(
                                instrument,
                                new InstrumentParams
                                {
                                    Instrument =
                                        new Instrument
                                        {
                                            Code = code,
                                            //TODO ClassCode?
                                            //ClassCode = sectionFeedAdapter.ClassCode(row)
                                        },
                                    DecimalPlaces = (uint)sectionFeedAdapter.GetDecimals(row)
                                });

                        Logger.Info().Print($"Adding instruments {classCode} {instrument}");
                    }

                    UpdateInstrumentParams(infoMteTable);

                    // если это фонда, то нужно ещё индексы подрубить
                    if (section == MicexSecionType.Stock)
                    {
                        var stockAdapter = (StockFeedAdapter)sectionFeedAdapter;

                        stockIndexesTableIdx = _connector.ApiWrapper.OpenTable(
                            tableTypes[stockAdapter.StockIndexesTableIndex],
                            "",
                            true,
                            out infoMteTable);

                        foreach (var row in infoMteTable.Rows)
                        {
                            var code = stockAdapter.GetIndexCode(row);
                            var instrument = new Instrument
                            {
                                Code = code,
                                // TODO ClassCode?
                                //ClassCode = "INDEX"
                            };

                            if (!_instrumentsParams.ContainsKey(instrument))
                                _instrumentsParams.Add(
                                    instrument,
                                    new InstrumentParams
                                    {
                                        Instrument =
                                            new Instrument
                                            {
                                                Code = code,
                                                // TODO ClassCode?
                                                //ClassCode = "INDEX"
                                            },
                                        DecimalPlaces = 2
                                    });

                            Logger.Info().Print($"Adding instrument INDEX {instrument}");
                        }
                    }

                    #endregion

                    #region Открываем таблицу всех сделок и сохраняем все сделки в TradesCollector

                    MTETable dealMteTable;
                    dealTableIdx = _connector.ApiWrapper.OpenTable(
                        tableTypes[sectionFeedAdapter.AllDealsTableIndex],
                        string.Empty,
                        false,
                        out dealMteTable);

                    // TODO TradesCollector?
                    //TradesCollector.AddClass2ClassCodeMapItem("FOB", "FOB");
                    var trades = sectionFeedAdapter.GetTradesFromTable(dealMteTable);
                    //TradesCollector.AddTrades(trades);
                    Logger.Info().Print($"{trades.Count()} сделок с начала сессии.");

                    #endregion

                    MTETable quoteMteTable;

                    quoteTableIdx = _connector.ApiWrapper.OpenTable(
                        tableTypes[sectionFeedAdapter.QuoteTableIndex],
                        sectionFeedAdapter.QuoteTableParams,
                        true,
                        out quoteMteTable);

                    sectionFeedAdapter.GetOrderBooksFromTable(quoteMteTable, _instrumentsParams, _orderBooks);
                }
                Logger.Info().Print("Запускаем задачу обновления данных.");
                dataRefreshementTask.Start();
            }
            catch (Exception exception)
            {
                Logger.Fatal().Print(exception,
                    $"Ошибка при запуске MicexFeed:{sectionFeedAdapter.GetType()}: {exception.Message},\nStack:\n{exception.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Останавливает фид данных.
        /// </summary>
        public override void Stop()
        {
            try
            {
                Logger.Info().Print("Останавливаем MicexFeed.");

                cancelSource.Cancel();
                try
                {
                    dataRefreshementTask.Wait();
                }
                catch (AggregateException)
                {
                }

                if (_connector.ApiWrapper != null)
                {
                    lock (_connector.ApiWrapper)
                    {
                        // закрываем все открытые таблицы
                        _connector.ApiWrapper.CloseTable(infoTableIdx);
                        _connector.ApiWrapper.CloseTable(dealTableIdx);

                        _connector.ApiWrapper.CloseTable(quoteTableIdx);

                        // отключаемся от mtesrl.dll.
                        _connector.ApiWrapper.Disconnect();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Fatal().Print(exception,
                    $"Ошибка при остановке MicexFeed:{sectionFeedAdapter.GetType()}: {exception.Message}, \n Stack:\n{exception.StackTrace}");
            }

            Logger.Info().Print("MicexFeed остановлен.");
        }

        #endregion

        /// <summary>
        /// В этом методе в цикле происходит обновление данных.
        /// </summary>
        private void CycleProc()
        {
            try
            {
                StockFeedAdapter stockFeedAdapter = null;

                if (section == MicexSecionType.Stock)
                    stockFeedAdapter = (StockFeedAdapter)sectionFeedAdapter;

                while (!token.IsCancellationRequested)
                {
                    lock (syncRoot)
                    {
                        MTETable[] mteTables;

                        lock (_connector.ApiWrapper)
                        {
                            _connector.ApiWrapper.AddTable(infoTableIdx, sectionFeedAdapter.InfoTableIndex);
                            _connector.ApiWrapper.AddTable(dealTableIdx, sectionFeedAdapter.AllDealsTableIndex);

                            if (section == MicexSecionType.Stock)
                                _connector.ApiWrapper.AddTable(stockIndexesTableIdx, stockFeedAdapter.StockIndexesTableIndex);

                            mteTables = _connector.ApiWrapper.Refresh(tableTypes);
                        }

                        ProcessTables(stockFeedAdapter, mteTables);

                        lock (_connector.ApiWrapper)
                        {
                            _connector.ApiWrapper.AddTable(quoteTableIdx, sectionFeedAdapter.QuoteTableIndex);
                            mteTables = _connector.ApiWrapper.Refresh(tableTypes);
                        }

                        ProcessTables(stockFeedAdapter, mteTables);                       
                    }

                    Thread.Sleep((int)refreshTimeout);
                }
            }
            catch (Exception exception)
            {
                Logger.Fatal().Print(exception,
                    $"Ошибка в задаче обновления фида MicexFeed:{sectionFeedAdapter.GetType()}: {exception.Message},\n Stack:\n{exception.StackTrace}");
            }
        }

        private void ProcessTables(StockFeedAdapter stockFeedAdapter, IEnumerable<MTETable> mteTables)
        {
            foreach (var table in mteTables)
            {
            	if (table.Ref == sectionFeedAdapter.QuoteTableIndex)
            	{
            	    var books = sectionFeedAdapter.GetOrderBookUpdatesFromTable(table, _instrumentsParams, _orderBooks);
                    foreach (var orderBook in books)
                    {
                        OnMessageReceived(orderBook.Clone());
                    } 
                }
                else if (table.Ref == sectionFeedAdapter.AllDealsTableIndex)
                {
                    var newTrades = sectionFeedAdapter.GetTradesFromTable(table);
                    // TODO TradesCollector?
                    //TradesCollector.AddTrades(newTrades);

                    foreach (var trade in newTrades)
                        OnMessageReceived(trade);
                }
                else if (table.Ref == sectionFeedAdapter.InfoTableIndex)
                {
                    foreach (var instrumentMessage in UpdateInstrumentParams(table))
                    {
                        OnMessageReceived(instrumentMessage);
                    }
                }
                else if (table.Ref == stockFeedAdapter.StockIndexesTableIndex)
                {
                    foreach (var instrumentMessage in UpdateIndexParams(table))
                    {
                        OnMessageReceived(instrumentMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Метод обновляет параметры инструментов изменившимися полями. Таблица параметров устроена так,
        /// что с каждым обновлением могут приходить разные столбцы, а не вся строка. Таким образом экономится траффик,
        /// однако мы не можем из пришедшей строки создать полные параметры инструментов. Поэтому приходится обновлять
        /// их, а не генерировать каждый раз новые.
        /// </summary>
        /// <param name="table">Таблица новых данных.</param>
        /// <returns></returns>
        private IEnumerable<InstrumentMessage> UpdateInstrumentParams(MTETable table)
        {
            var rValue = new List<InstrumentMessage>();

            foreach (var currRow in table.Rows)
            {
                var instrument = new Instrument
                {
                    Code = sectionFeedAdapter.GetInstrumentCodeFromParams(currRow),
                    // TODO ClassCode
                    //ClassCode = sectionFeedAdapter.ClassCode(currRow)
                };
                var instrumentParams = _instrumentsParams[instrument];

                sectionFeedAdapter.UpdateInstrumentParams(currRow, instrumentParams);

                if (!rValue.Contains(instrumentParams))
                    rValue.Add(instrumentParams);
            }

            return rValue;
        }

        /// <summary>
        /// Метод обновляет параметры индексов изменившимися полями. Таблица параметров устроена так,
        /// что с каждым обновлением могут приходить разные столбцы, а не вся строка. Таким образом экономится траффик,
        /// однако мы не можем из пришедшей строки создать полные параметры инструментов. Поэтому приходится обновлять
        /// их, а не генерировать каждый раз новые.
        /// </summary>
        /// <param name="table">Таблица новых данных.</param>
        /// <returns></returns>
        private IEnumerable<InstrumentMessage> UpdateIndexParams(MTETable table)
        {
            var rValue = new List<InstrumentMessage>();

            var stockSectionAdapter = (StockFeedAdapter)sectionFeedAdapter;

            foreach (var currRow in table.Rows)
            {
                var instrument = new Instrument
                {
                    Code = stockSectionAdapter.GetIndexCode(currRow),
                    // TODO ClassCode?
                    //ClassCode = "INDEX"
                };

                var instrumentParams = _instrumentsParams[instrument];

                // TODO CurrentValue?
                //instrumentParams.CurrentValue = stockSectionAdapter.GetIndexValue(currRow, 100);
                //instrumentParams.LastChangeTime = DateTime.Today + stockSectionAdapter.GetIndexLastChangeTime(currRow);

                if (!rValue.Contains(instrumentParams))
                    rValue.Add(instrumentParams);
            }

            return rValue;
        }

        #region Connector.Feed, IOrderBookSubscriber, IInstrumentParamsSubscriber, IFortsDataProvider, IInstrumentTickerLookup

        public void SubscribeOrderBook(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOrderBook(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionResult> Subscribe(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public string QueryFullCode(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public string[] LookupInstruments(string code, int maxResults = 10)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
