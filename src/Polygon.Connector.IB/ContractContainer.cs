using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using IBApi;
using ITGlobal.DeadlockDetection;
using JetBrains.Annotations;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Пенис-холдер для контрактов
    /// </summary>
    internal sealed class ContractContainer
    {
        #region nested PendingContract class

        /// <summary>
        ///     Контракт, ожидающий резолва
        /// </summary>
        private sealed class PendingContract : TaskCompletionSource<Contract>
        {
            public PendingContract(int tickerId)
            {
                TickerId = tickerId;
            }

            public int TickerId { get; }
        }

        #endregion

        #region fields

        private static readonly ILog _Log = LogManager.GetLogger<ContractContainer>();

        private readonly IBAdapter adapter;

        private readonly IRwLockObject containerLock = DeadlockMonitor.ReaderWriterLock<ContractContainer>("containerLock", LockRecursionPolicy.SupportsRecursion);
        private readonly Dictionary<int, Instrument> pendingContractTickerIds = new Dictionary<int, Instrument>();
        private readonly Dictionary<Instrument, PendingContract> pendingContracts = new Dictionary<Instrument, PendingContract>();
        private readonly Dictionary<Instrument, Contract> contractByInstrument = new Dictionary<Instrument, Contract>();

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="adapter">
        ///     Адаптер IB
        /// </param>
        public ContractContainer(IBAdapter adapter, IBParameters.IBInstrumentConverter instrumentConverter)
        {
            this.adapter = adapter;
            InstrumentConverter = instrumentConverter;
        }

        #endregion

        public IBParameters.IBInstrumentConverter InstrumentConverter { get; }

        #region public methods

        /// <summary>
        ///     Запросить контракт по инструменту (асинхронно)
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="contractStub">
        ///     Существующий объект <see cref="Contract"/>, для которого уточнается часть полей
        /// </param>
        /// <param name="cancellationToken">
        ///     Токен отмены
        /// </param>
        public async Task<Contract> GetContractAsync(Instrument instrument, Contract contractStub = null, CancellationToken cancellationToken = new CancellationToken())
        
        {
            using (LogManager.Scope())
            {
                using (containerLock.UpgradableReadLock())
                {
                    // Запрашиваем контракт из кеша
                    Contract contract;
                    if (contractByInstrument.TryGetValue(instrument, out contract))
                    {
                        return contract;
                    }
                }
                // Запрашиваем контракт
                return await RequestContract(instrument, contractStub, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Запросить контракт по инструменту (синхронно)
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <returns>
        ///     Контракт, если он уже определен. null, если контракт еще не резолвился
        /// </returns>
        [CanBeNull]
        public Contract GetContract(Instrument instrument)
        {
            using (containerLock.UpgradableReadLock())
            {
                // Ищем контракт
                Contract contract;
                if (contractByInstrument.TryGetValue(instrument, out contract))
                {
                    return contract;
                }
            }

            // Если контракт не найден - запрашиваем его
            RequestContract(instrument).Ignore();

            return null;
        }

        /// <summary>
        ///     Запросить инструмент по контракту (асинхронно)
        /// </summary>
        /// <param name="contract">
        ///     Контракт
        /// </param>
        /// <param name="objectDescription">
        ///     Описание объекта, зависящего от контракта
        /// </param>
        /// <param name="continuation">
        ///     Асинхронный коллбек. В случае неудачи резолва инструмента не вызывается.
        /// </param>
        public async void GetInstrumentAsync(Contract contract, string objectDescription, Action<Instrument> continuation)
        {
            using (LogManager.Scope())
            {
                try
                {
                    string code;
                    Instrument instrument = null;
                    var tryNumber = 0;

                    // Если поле Exchange не указано в contract, тогда нужно сделать дополнительный запрос
                    if (string.IsNullOrEmpty(contract.Exchange))
                    {
                        // Используем временный инструмент с заведомо уникальным кодом
                        contract = await RequestContract(new Instrument { Code = $"TEMPORARY_INSTRUMENT_{Guid.NewGuid():N}" }, contract);
                    }

                    // Попытка 1 - сначала пытаемся найти по коду с доп.полями и с указанием родной биржи
                    // Поиск будет идти только по локальному кэшу конвертера
                    if (!string.IsNullOrEmpty(contract.Exchange))
                    {
                        code = $"exchange:{contract.Exchange};{contract.LocalSymbol}";
                        instrument = await InstrumentConverter.ResolveSymbolAsync(code, objectDescription).ConfigureAwait(false);
                        tryNumber++;
                        if (instrument != null)
                        {
                            continuation(instrument);
                            _Log.Debug().Print(
                                $"Instrument {contract.LocalSymbol} was resolved by InstrumentConverter as {instrument}.",
                                LogFields.Code(code),
                                LogFields.Symbol(contract.LocalSymbol),
                                LogFields.Exchange(contract.Exchange),
                                LogFields.Instrument(instrument),
                                LogFields.Attempt(tryNumber)
                                );
                            return;
                        }
                    }

                    // Попытка 2 - сначала пытаемся найти по коду с доп.полями и с указанием квазибиржи SMART или GLOBEX
                    // поиск будет идти только по локальному кэшу конвертера
                    var exchangeCode = contract.SecType == "FUT" || contract.SecType == "FOP" ? GLOBEX : SMART;
                    code = $"exchange:{exchangeCode};{contract.LocalSymbol}";
                    instrument = await InstrumentConverter.ResolveSymbolAsync(code, objectDescription).ConfigureAwait(false);
                    tryNumber++;
                    if (instrument != null)
                    {
                        continuation(instrument);
                        _Log.Debug().Print(
                            $"Instrument {contract.LocalSymbol} was resolved by InstrumentConverter as {instrument}.",
                            LogFields.Code(code),
                            LogFields.Symbol(contract.LocalSymbol),
                            LogFields.Exchange(contract.Exchange),
                            LogFields.Instrument(instrument),
                            LogFields.Attempt(tryNumber)
                            );
                        return;
                    }


                    // Попытка 3 - ищем просто по коду
                    code = contract.LocalSymbol;
                    instrument = await InstrumentConverter.ResolveSymbolAsync(code, objectDescription).ConfigureAwait(false);
                    tryNumber++;
                    if (instrument != null)
                    {
                        continuation(instrument);
                        _Log.Debug().Print(
                            $"Instrument {contract.LocalSymbol} was resolved by InstrumentConverter as {instrument}.",
                            LogFields.Code(code),
                            LogFields.Symbol(contract.LocalSymbol),
                            LogFields.Exchange(contract.Exchange),
                            LogFields.Instrument(instrument),
                            LogFields.Attempt(tryNumber)
                            );
                        return;
                    }

                    _Log.Error().Print(
                           $"Instrument {contract.LocalSymbol} wasn't resolved by InstrumentConverter.",
                           LogFields.Code(code),
                           LogFields.Symbol(contract.LocalSymbol),
                           LogFields.Exchange(contract.Exchange),
                           LogFields.Attempt(tryNumber)
                           );
                }
                catch (Exception e)
                {
                    _Log.Error().Print(e, $"Failed to get instrument from {contract}");
                }
            }
        }

        /// <summary>
        ///     Обработать результат резолва контракта
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="contract">
        ///     Контракт
        /// </param>
        public void PutContract(Instrument instrument, Contract contract)
        {
            using (containerLock.WriteLock())
            {
                if (contractByInstrument.ContainsKey(instrument))
                {
                    return;
                }

                // Записываем контракт 
                contractByInstrument.Add(instrument, contract);

                // Обрабатываем отложенный контракт
                PendingContract pendingContract;
                if (pendingContracts.TryGetValue(instrument, out pendingContract))
                {
                    pendingContract.TrySetResultBackground(contract);
                    pendingContracts.Remove(instrument);

                    pendingContractTickerIds.Remove(pendingContract.TickerId);
                }
            }
        }

        /// <summary>
        ///     Обработать отказ резолва инструмента
        /// </summary>
        /// <param name="tickerId">
        ///     Номер тикера
        /// </param>
        public void RejectContract(int tickerId)
        {
            using (containerLock.WriteLock())
            {
                Instrument instrument;
                if (!pendingContractTickerIds.TryGetValue(tickerId, out instrument))
                {
                    return;
                }

                pendingContractTickerIds.Remove(tickerId);
                pendingContracts.Remove(instrument);
            }
        }

        #endregion

        #region implementation details

        private async Task<Contract> RequestContract(Instrument instrument, Contract contractStub = null, CancellationToken cancellationToken = new CancellationToken())
        {
            using (LogManager.Scope())
            {
                PendingContract pendingContract;

                using (containerLock.WriteLock())
                {
                    pendingContracts.TryGetValue(instrument, out pendingContract);
                }

                if (pendingContract != null)
                {
                    return await pendingContract.Task.ConfigureAwait(false);
                }


                // Такого контракта еще нет в списке ожидающих
                // Запрашиваем контракт из адаптера, используя фейковый контракт
                var contract = contractStub ?? await ResolveContractStubAsync(instrument).ConfigureAwait(false);

                if (contract != null)
                {
                    var tickerId = adapter.RequestContractDetails(instrument, contract);

                    using (containerLock.WriteLock())
                    {
                        // Сохраняем номер тикера
                        pendingContractTickerIds.Add(tickerId, instrument);

                        pendingContract = new PendingContract(tickerId);
                        pendingContracts.Add(instrument, pendingContract);
                    }

                    return await pendingContract.Task.ConfigureAwait(false);
                }

                return null;
            }
        }

        private async Task<Contract> ResolveContractStubAsync(Instrument instrument)
        {
            using (LogManager.Scope())
            {
                var symbol = await InstrumentConverter.ResolveInstrumentAsync(instrument, false).ConfigureAwait(false);
                if (symbol == null)
                {
                    _Log.Error().Print("Unable to resolve vendor symbol for an instrument", LogFields.Instrument(instrument));
                    return null;
                }

                var instrumentData = await InstrumentConverter.ResolveInstrumentAsync(instrument);
                var contract = GetContractStub(instrumentData);

                return contract;
            }
        }

        internal Task<Tuple<bool, string>> TestAssetContractAsync(string symbol, IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL

            if (symbol == null)
            {
                return Task.FromResult(Tuple.Create(false, ""));
            }

            var contract = GetAssetContractStub(instrumentData);
            var testResult = adapter.TestContract(contract);
            var result = testResult.WaitAsync();
            return Task.FromResult(Tuple.Create(result.Result, ""));
        }

        internal Task<Tuple<bool, string>> TestFutureContractAsync(string symbol, IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL

            if (symbol == null)
            {
                return Task.FromResult(Tuple.Create(false, ""));
            }

            var contract = GetFutureContractStub(instrumentData);
            var testResult = adapter.TestContract(contract);
            var result = testResult.WaitAsync();
            return Task.FromResult(Tuple.Create(result.Result, ""));
        }

        internal Task<Tuple<bool, string>> TestAssetOptionContractAsync(string symbol, IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL

            if (symbol == null)
            {
                return Task.FromResult(Tuple.Create(false, ""));
            }

            var contract = GetAssetOptionContractStub(instrumentData);
            var testResult = adapter.TestContract(contract);
            var result = testResult.WaitAsync();
            return Task.FromResult(Tuple.Create(result.Result, ""));
        }

        internal Task<Tuple<bool, string>> TestFutureOptionContractAsync(string symbol, IBInstrumentData instrumentData)
        {
            if (symbol == null)
            {
                return Task.FromResult(Tuple.Create(false, ""));
            }

            var contract = GetFutureOptionContractStub(instrumentData);
            var testResult = adapter.TestContract(contract);
            var result = testResult.WaitAsync();
            return Task.FromResult(Tuple.Create(result.Result, ""));
        }

        // ReSharper disable InconsistentNaming
        private const string SMART = "SMART";
        private const string GLOBEX = "GLOBEX";
        // ReSharper restore InconsistentNaming

        private static readonly Regex _SymbolRegex = new Regex(@"^(exchange:(?<exchange>[A-Z0-9]+);)?(?<symbol>[A-Z0-9\s\.]+)$");

        /// <summary>
        ///     Если узел принадлежит пользовательской (созданной руками) бирже, метод возвращает её код. 
        ///     Для бирж из символ-сервиса возвращается <paramref name="defaultExchange"/>.
        /// </summary>
        private static void GetCustomSymbolAndExchangeCode([CanBeNull]IBInstrumentData instrumentData, string defaultExchange, ref string symbol, [NotNull] out string exchangeCode)
        {
            var match = _SymbolRegex.Match(symbol);

            // Если площадка явно указана в символе
            if (match.Success)
            {
                symbol = match.Groups["symbol"].Value;
                exchangeCode = match.Groups["exchange"].Value;

                if (!string.IsNullOrEmpty(exchangeCode))
                {
                    return;
                }
            }
            
            if (instrumentData == null)
            {
                exchangeCode = defaultExchange;
                return;
            }

            exchangeCode = instrumentData?.ExchangeCode ?? defaultExchange;

            // Хардкод для CBOT
            if (exchangeCode == "CBT")
            {
                exchangeCode = "ECBOT";
                return;
            }

            if (exchangeCode == "HKEX")
            {
                exchangeCode = "HKFE";
                return;
            }

            // Для опционов и фьючерсов не CBOT
            InstrumentType instrumentType = instrumentData.InstrumentType;
            if (instrumentType == InstrumentType.Option 
                || instrumentType == InstrumentType.AssetOptionSeries
                || instrumentType == InstrumentType.FutureOptionSeries
                || instrumentType == InstrumentType.Future)
            {
                exchangeCode = defaultExchange;
                return;
            }

            // Для акций
            AssetType assetType = instrumentData.AssetType;
            if (assetType == AssetType.Equity)
            { 
                exchangeCode = defaultExchange;
            }
        }

        private static Contract GetContractStub(IBInstrumentData instrumentData)
        {
            switch (instrumentData.InstrumentType)
            {
                case InstrumentType.Asset:
                    return GetAssetContractStub(instrumentData);

                case InstrumentType.Future:
                    return GetFutureContractStub(instrumentData);

                case InstrumentType.FutureOptionSeries:
                    return GetFutureOptionContractStub(instrumentData);
                    
                case InstrumentType.AssetOptionSeries:
                    return GetAssetOptionContractStub(instrumentData);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Contract GetAssetContractStub(IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL
            string exchangeCode;
            string symbol = instrumentData.Symbol;
            GetCustomSymbolAndExchangeCode(instrumentData, SMART, ref symbol, out exchangeCode);
            var contract = new Contract
            {
                Exchange = exchangeCode,
                LocalSymbol = symbol,
                PrimaryExch = TryGetPrimaryExch(instrumentData)
            };

            var assetType = instrumentData.AssetType;
            switch (assetType)
            {
                case AssetType.Index:
                    contract.SecType = "IND";
                    break;
                case AssetType.FX:
                    contract.SecType = "CASH";
                    break;
                default:
                    contract.SecType = "STK";
                    break;
            }

            return contract;
        }

        private static Contract GetFutureContractStub(IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL
            string exchangeCode;
            string symbol = instrumentData.Symbol;
            GetCustomSymbolAndExchangeCode(instrumentData, GLOBEX, ref symbol, out exchangeCode);
            var contract = new Contract
            {
                Exchange = exchangeCode,
                LocalSymbol = symbol,
                SecType = "FUT"
            };
            return contract;
        }

        private static Contract GetAssetOptionContractStub(IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL
            string exchangeCode;
            string symbol = instrumentData.Symbol;
            GetCustomSymbolAndExchangeCode(instrumentData, SMART, ref symbol, out exchangeCode);
            var contract = new Contract
            {
                Exchange = exchangeCode,
                LocalSymbol = symbol,
                SecType = "OPT"
            };
            return contract;
        }

        private static Contract GetFutureOptionContractStub(IBInstrumentData instrumentData)
        {
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL
            string exchangeCode;
            string symbol = instrumentData.Symbol;
            GetCustomSymbolAndExchangeCode(instrumentData, GLOBEX, ref symbol, out exchangeCode);
            var contract = new Contract
            {
                Exchange = exchangeCode,
                LocalSymbol = symbol,
                SecType = "FOP"
            };
            return contract;
        }

        private static string TryGetPrimaryExch(IBInstrumentData instrumentData)
        {
            var exchange = instrumentData.ExchangeCode;
            switch (exchange)
            {
                case "NYSE_ARCA":
                    return "ARCA";

                case "NYSE_MKT":
                    return "AMEX";

                case "NCM":
                case "NGM":
                case "NGSM":
                case "NSDQ":
                    return "NASDAQ";

                case "NYSE":
                    return "NYSE";

                default:
                    return null;
            }
        }

        #endregion
    }
}

