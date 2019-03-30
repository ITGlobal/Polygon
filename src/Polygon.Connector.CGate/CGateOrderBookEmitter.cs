using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CGateAdapter.Messages;
using CGateAdapter.Messages.OrdersAggr;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// Класс занимается сборкой стаканов, приходящих от cgate-а
    /// </summary>
    internal sealed class CGateOrderBookEmitter  
    {
        #region Fields

        private readonly ConcurrentQueue<CGateOrderBookUpdate> orderBooksUdatesQueue = new ConcurrentQueue<CGateOrderBookUpdate>();

        /// <summary>
        /// Мэп id записи об обновлении стакана на isinId интрумента. Нужен для обработки ситуации, когда приходит запись об обновлении
        /// строки с id, который встречался ранее, но у которой изменился инструмент, стакан которого она обновляет
        /// ReplId -> IsinId
        /// </summary>
        private readonly Dictionary<long, int> obRowsOnInstruments = new Dictionary<long, int>();

        /// <summary>
        /// IsinId -> ReplId -> OrderBookItem
        /// </summary>
        private readonly Dictionary<int, Dictionary<long, OrderBookItem>> plaza2OrderBooks = new Dictionary<int, Dictionary<long, OrderBookItem>>();

        /// <summary>
        /// isinId на стакан
        /// IsinId -> OrderBook
        /// </summary>
        private readonly Dictionary<int, OrderBook> orderBooks = new Dictionary<int, OrderBook>();

        ///// <summary>
        ///// ReplId -> ReplRev
        ///// </summary>
        //private readonly Dictionary<long, long> revisionsDictionary = new Dictionary<long, long>();

        /// <summary>
        /// Фоновая задача сборки стаканов
        /// </summary>
        private Task orderBookUpdatesProcessingTask;

        /// <summary>
        /// Флаг остановки фоновых задач
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;
      
        /// <summary>
        /// Список инструментов, чьи стаканы обновились в текущем цикле
        /// </summary>
        private readonly List<int> updatedInstruments = new List<int>();

        private readonly string streamName;
        private readonly CGateInstrumentResolver instrumentResolver;

        #endregion

        #region .ctor

        public CGateOrderBookEmitter(string streamName, CGateInstrumentResolver instrumentResolver)
        {
            this.streamName = streamName;
            this.instrumentResolver = instrumentResolver;
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие обновления стакана
        /// </summary>
        public event EventHandler<OrderBookUpdatedEventArgs> OrderBookUpdated;

        /// <summary>
        /// Вызвать событие обновления стакана
        /// </summary>
        private void OnOrderBookUpdated(OrderBookUpdatedEventArgs eventArgs)
        {
            var handler = OrderBookUpdated;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        #endregion

        #region Public methods

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            orderBookUpdatesProcessingTask = Task.Factory.StartNew(ProcessOrderBookUpdates);
        }

        public void Stop()
        {
            cancellationTokenSource?.Cancel();
            orderBookUpdatesProcessingTask?.Wait();
        }

        /// <summary>
        /// Обработчик сообщения cgate об обновлении строки стакана
        /// </summary>
        public void Handle(CgmOrdersAggr record)
        {
            if (record.StreamName != streamName)
                return;

            var code = instrumentResolver.GetShortIsinByIsinId(record.IsinId);
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException($"Can't resolve instrument code for isinId={record.IsinId}");
            }

            orderBooksUdatesQueue.Enqueue(new CGateOrderBookUpdate(record, code));
        }

        public void Handle(CGateDataEnd message)
        {
            if (message.StreamName != streamName)
                return;

            orderBooksUdatesQueue.Enqueue(new CGateOrderBookUpdate(CGateOrderBookUpdateType.StreamDataEnd));
        }

        public void Handle(CGateClearTableMessage message)
        {
            if (message.StreamName != streamName)
                return;

            orderBooksUdatesQueue.Enqueue(new CGateOrderBookUpdate(message));
        }

        /// <summary>
        /// Возвращает актуальный стакан для инструмента
        /// </summary>
        public OrderBook GetOrderBook(Instrument instrument)
        {
            InstrumentData data = instrumentResolver.GetCGateInstrumentData(instrument);
            var isinId = instrumentResolver.GetIsinIdByShortIsin(data.Symbol ?? "");
            if (isinId == int.MinValue)
            {
                return null;
            }

            OrderBook ob;
            orderBooks.TryGetValue(isinId, out ob);
            return ob;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Отправить подписчикам обновлёные стаканы
        /// </summary>
        private void FireUpdatedOrderBooks()
        {
            if (updatedInstruments.Count > 0)
            {
                var updatedBooks = updatedInstruments.Select(isinId => orderBooks[isinId]).ToList();
                updatedInstruments.Clear();
                OnOrderBookUpdated(new OrderBookUpdatedEventArgs { Books = updatedBooks });
            }
        }

        private void ClearOrderBooks(CGateOrderBookUpdate record)
        {
            //if (record.ReplRev != long.MaxValue)
            //{
            //    return;
            //    //throw new ArgumentException("В CGateClearTableMessage record.ReplRev != long.MaxValue", nameof(record.ReplRev));
            //}

            plaza2OrderBooks.Clear();
            orderBooks.Clear();
            obRowsOnInstruments.Clear();

            //var replIdsToRemove = new HashSet<long>(revisionsDictionary
            //    .Where(_ => _.Value < record.ReplRev)
            //    .Select(_ => _.Key));

            //replIdsToRemove.ForEach(_ => revisionsDictionary.Remove(_));

            //foreach (var plaza2OrderBook in plaza2OrderBooks)
            //{
            //    var isinId = plaza2OrderBook.Key;
            //    var p2ob = plaza2OrderBook.Value;

            //    var keysToRemove = p2ob.Keys
            //        .Where(_ => replIdsToRemove.Contains(_))
            //        .ToArray();

            //    OrderBook ob = orderBooks[isinId];
            //    //if (orderBooks.TryGetValue(isinId, out ob))
            //    //{
            //    foreach (var replId in keysToRemove)
            //    {
            //        var p2obi = p2ob[replId];
            //        p2ob.Remove(replId);

            //        var obIndex = ob.Items.IndexOf(_ => _.Price == p2obi.Price);
            //        ob.Items.RemoveAt(obIndex);
            //    }
            //    //}
            //}

        }

        /// <summary>
        /// Запоминает, что инструмент был обновлён. В событие об обновлении стаканов будет вызвано только для соотв. инструментов
        /// </summary>
        private void RememberUpdatedInstrument(int isinId)
        {
            updatedInstruments.Add(isinId);
        }

        private void ProcessOrderBookUpdates()
        {
            const int numberOfCyclesToFireEvent = 10; // каждые 10 циклов нижлежащего while мы райзим эвент об обновлении стаканов
            var cyclesCount = 0;

            while (!cancellationTokenSource.Token.WaitHandle.WaitOne(10))
            {
                if (++cyclesCount > numberOfCyclesToFireEvent)
                {
                    cyclesCount = 0;
                    FireUpdatedOrderBooks();
                }

                CGateOrderBookUpdate record;

                while (orderBooksUdatesQueue.TryDequeue(out record))
                {
                    // вызываем событие обновления стаканов если в потоке пришло сообщение об окончании блока обновлений
                    if (record.Type == CGateOrderBookUpdateType.StreamDataEnd)
                    {
                        FireUpdatedOrderBooks();
                        continue;
                    }

                    if (record.Type == CGateOrderBookUpdateType.ClearTable)
                    {
                        ClearOrderBooks(record);
                        FireUpdatedOrderBooks();
                        continue;
                    }

                    //revisionsDictionary[record.ReplId] = record.ReplRev;
                    RememberUpdatedInstrument(record.IsinId);

                    int obItemIsinId;
                    var instrumentChanged = false;

                    if (!obRowsOnInstruments.TryGetValue(record.ReplId, out obItemIsinId))
                    {
                        obRowsOnInstruments.Add(record.ReplId, record.IsinId);
                    }
                    else if (record.IsinId != obItemIsinId)
                    {
                        RememberUpdatedInstrument(obItemIsinId);
                        obRowsOnInstruments[record.ReplId] = record.IsinId;
                        instrumentChanged = true;
                    }

                    Dictionary<long, OrderBookItem> p2ob;
                    OrderBook ob;

                    if (!plaza2OrderBooks.TryGetValue(record.IsinId, out p2ob))
                    {
                        p2ob = new Dictionary<long, OrderBookItem>();
                        plaza2OrderBooks.Add(record.IsinId, p2ob);
                    }

                    if (!orderBooks.TryGetValue(record.IsinId, out ob))
                    {
                        var instrument = instrumentResolver.GetInstrument(record.InstrumentCode);
                        ob = new OrderBook {Instrument = instrument.Instrument};
                        orderBooks.Add(record.IsinId, ob);
                    }

                    // если произошла смена инструмента у записи, то её нужно удалить из того стакана, в котором она была раньше
                    if (instrumentChanged)
                    {
                        Dictionary<long, OrderBookItem> p2obToDeleteRecord;
                        OrderBook obToDeleteRecord;
                        
                        if (plaza2OrderBooks.TryGetValue(obItemIsinId, out p2obToDeleteRecord) &&
                            orderBooks.TryGetValue(obItemIsinId, out obToDeleteRecord))
                        {
                            if (p2obToDeleteRecord.ContainsKey(record.ReplId))
                            {
                                obToDeleteRecord.Items.Remove(p2obToDeleteRecord[record.ReplId]);
                                p2obToDeleteRecord.Remove(record.ReplId);
                            }
                        }
                    }

                    OrderBookItem obi;

                    if (!p2ob.TryGetValue(record.ReplId, out obi) && record.Price != 0m && record.Quantity != 0 && record.ReplAct == 0)
                    {
                        obi = new OrderBookItem { Price = record.Price, Quantity = record.Quantity, Operation = record.Operation };

                        // ишем место, куда приткнуть новую котировку
                        InsertOrderBookItemSorted(record.ReplId, p2ob, ob, obi);
                    }
                    else if (obi != null)
                    {
                        if (record.Price == 0 || record.Quantity == 0 || record.ReplAct > 0)
                        {
                            RemoveOrderBookItem(record.ReplId, p2ob, ob);
                        }
                        else
                        {
                            var needSort = obi.Price != record.Price || obi.Operation != record.Operation;

                            obi.Price = record.Price;
                            obi.Operation = record.Operation;
                            obi.Quantity = record.Quantity;

                            if (needSort)
                            {
                                RemoveOrderBookItem(record.ReplId, p2ob, ob);
                                InsertOrderBookItemSorted(record.ReplId, p2ob, ob, obi);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вставляет с сортировкой запись в стакан.
        /// </summary>
        /// <param name="replId">replId записи из plaza2.</param>
        /// <param name="p2Ob">Мэп replId на OrderBookItem.</param>
        /// <param name="ob">Объект стакана.</param>
        /// <param name="obi">Запись в стакане.</param>
        private void InsertOrderBookItemSorted(long replId, Dictionary<long, OrderBookItem> p2Ob, OrderBook ob, OrderBookItem obi)
        {
            p2Ob.Add(replId, obi);

            var indexToInsert = 0;

            if (ob.Items.Count > 0)
            {
                for (indexToInsert = 0; indexToInsert < ob.Items.Count; indexToInsert++)
                {
                    if (obi.Price > ob.Items[indexToInsert].Price)
                        break;
                }
            }

            ob.Items.Insert(indexToInsert, obi);
        }

        /// <summary>
        /// Удаляет запись из стакана.
        /// </summary>
        /// <param name="replId">replId записи из plaza2 для удаления.</param>
        /// <param name="p2Ob">Мэп replId на OrderBookItem.</param>
        /// <param name="ob">Объект стакана.</param>
        private void RemoveOrderBookItem(long replId, Dictionary<long, OrderBookItem> p2Ob, OrderBook ob)
        {
            ob.Items.Remove(p2Ob[replId]);
            p2Ob.Remove(replId);
        }

        #endregion
    }
}

