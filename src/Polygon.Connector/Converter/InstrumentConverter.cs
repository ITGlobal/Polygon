using System;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Абстрактный класс для создания экземпляра в конкретном коннекторе реализующего:
    ///     - конвертирование внешнего инструмента в инструмент для конкретного транспорта в котором создан экземпляр конвертера;
    ///     - конвертирование вендорского кода инструмента в инструмент для конкретного транспорта в котором создан экземпляр конвертера;
    ///     - конвертирование инструмента для конкретного транспорта в котором создан экземпляр конвертера в его вендорский код;
    ///     - получение метаданных по инструменту для конкретного транспорта в котором создан экземпляр конвертера.
    /// </summary>
    /// <typeparam name="TConverter">
    ///     Тип класса для конкретного транспорта
    /// </typeparam>
    /// <typeparam name="TData">
    ///     Структура для хранения метаданных по инструменту
    /// </typeparam>
    /// <typeparam name="TAdapter">
    ///     Адаптер транспорта реализующий интерфейс ISubscriptionTester для тестирования подписки на инструмент по вендорскому коду
    /// </typeparam>
    public abstract class InstrumentConverter<TConverter, TData, TAdapter> 
        : IInstrumentConverter<TData> where TData : InstrumentData 
        where TConverter : InstrumentConverter<TConverter, TData, TAdapter>, new()
    {
        #region Fields

        private CacheDataByInstrument _cacheDataByInstrument;
        private CacheInstrumentBySymbol _cacheInstrumentBySymbol;
        private CacheSymbolByInstrument _cacheSymbolByInstrument;
        private CacheTransportInstrumentByExternalInstrument _cacheTransportInstrumentByExternalInstrument;

        private static readonly ILockObject SyncRoot = DeadlockMonitor.Cookie<TConverter>();
        private static TConverter _instance;

        /// <summary>
        ///     Интерфейс внешнего конвертера инструментов
        /// </summary>
        private IInstrumentConverter<TData> _externalConverter;

        #endregion

        #region Methods

        private Instrument GetInstrumentBySymbol(string symbol) 
            => _cacheInstrumentBySymbol[symbol];
        private string GetSymbolByInstrument(Instrument instrument) 
            => _cacheSymbolByInstrument[instrument];
        private TData GetDataByInstrument(Instrument instrument) 
            => _cacheDataByInstrument[instrument];
        private Instrument GetTransportInstrumentByExternalInstrument(Instrument externalInstrument) 
            => _cacheTransportInstrumentByExternalInstrument[externalInstrument];

        /// <summary>
        ///     Обновляет кэш после обращения к внешнему конвертеру инструментов
        /// </summary>
        /// <param name="data"></param>
        private void UpdateCache(TData data)
        {
            _cacheInstrumentBySymbol.UpdateCache(data.Symbol, data.TransportInstrument);
            _cacheSymbolByInstrument.UpdateCache(data.TransportInstrument, data.Symbol);
            _cacheDataByInstrument.UpdateCache(data.TransportInstrument, data);
        }

        /// <summary>
        ///     Обновляет кэш после обращения к внешнему конвертеру инструментов
        /// </summary>
        /// <param name="externalInstrument">
        ///     Внешний по отношению к конкретному транспорту инструмент
        /// </param>
        /// <param name="transportInstrument">
        ///     Инструмент конкретного транспорта
        /// </param>
        private void UpdateCache(Instrument externalInstrument, Instrument transportInstrument)
        {
            _cacheTransportInstrumentByExternalInstrument.UpdateCache(externalInstrument, transportInstrument);
        }

        #endregion

        #region .ctor

        /// <summary>
        ///    Конструктор 
        /// </summary>
        protected InstrumentConverter()
        {
        }

        /// <summary>
        ///     Возвращает ссылку на экземпляр класса для конвертирования вендорского кода инструмента в инструмент и наоборот
        /// </summary>
        /// <param name="externalConverter">
        ///     Интерфейс внешнего конвертера инструментов (можно не использовать)
        /// </param>
        /// <returns>
        ///     Возвращает ссылку на одиночный экземпляр создаваемого типа
        /// </returns>
        public static TConverter Create([CanBeNull]IInstrumentConverter<TData> externalConverter)
        {
            if (_instance == null)
            {
                using (SyncRoot.Lock())
                {
                    if (_instance == null)
                    {
                        _instance = new TConverter()
                        {
                            _externalConverter = externalConverter,
                            _cacheDataByInstrument = new CacheDataByInstrument(),
                            _cacheInstrumentBySymbol = new CacheInstrumentBySymbol(),
                            _cacheSymbolByInstrument = new CacheSymbolByInstrument(),
                            _cacheTransportInstrumentByExternalInstrument = new CacheTransportInstrumentByExternalInstrument()
                        };
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Class interface

        #region Properties

        /// <summary>
        ///     Адаптер транспорта реализующий интерфейс 
        ///     ISubscriptionTester для тестирования подписки на инструмент по вендорскому коду (optional)
        /// </summary>
        public TAdapter Adapter { set; private get; }

        #endregion

        /// <summary>
        ///     Разрешить инструмент по вендорскому коду
        /// </summary>
        /// <param name="symbol">
        ///     Вендорский код
        /// </param>
        /// <param name="dependentObjectDescription">
        ///     Строковое описание объекта, который зависит от инструмента
        /// </param>
        /// <returns>
        ///     Асихронный результат с инструментом
        /// </returns>
        public async Task<Instrument> ResolveSymbolAsync(string symbol, string dependentObjectDescription = null)
        {
            Instrument instrument = GetInstrumentBySymbol(symbol);

            if (instrument == null)
            {
                TData data = await (this as IInstrumentConverter<TData>).ResolveSymbolAsync(symbol, dependentObjectDescription);

                instrument = data.TransportInstrument;
                if (instrument != null)
                {
                    UpdateCache(data);
                }
            }

            return await Task.FromResult(instrument);
        }

        /// <summary>
        ///     Получить символ по инструменту
        /// </summary>
        /// <param name="instrument">
        /// </param>
        /// <param name="isTestVendorCodeRequired">
        ///     Флаг выполнения тестирования вендорского кода через адаптер транспорта
        /// </param>
        /// <returns>
        ///     Асихронный результат с вендорским кодом инструмента
        /// </returns>
        public async Task<string> ResolveInstrumentAsync(Instrument instrument, bool isTestVendorCodeRequired)
        {
            string symbol = GetSymbolByInstrument(instrument);

            if (string.IsNullOrEmpty(symbol))
            {
                TData data = await (this as IInstrumentConverter<TData>).ResolveInstrumentAsync(instrument);

                symbol = data.Symbol;
                if (!string.IsNullOrEmpty(symbol))
                {
                    if (isTestVendorCodeRequired)
                    {
                        if (Adapter == null)
                        {
                            throw new NotSupportedException($"Vendor code test not supported: Set current class Adapter property first");
                        }
                        if (Adapter is ISubscriptionTester)
                        {
                            ISubscriptionTester tester = (ISubscriptionTester)Adapter;
                            var test = await tester.TestVendorCodeAsync(symbol);
                            bool isResolved = test.Item1;
                            if (isResolved)
                            {
                                UpdateCache(data);
                            }
                            else
                            {
                                symbol = null;
                            }
                        }
                        else
                        {
                            throw new NotSupportedException($"Vendor code test not supported: {Adapter} doesn't implements ISubscriptionTester interface");
                        }
                    }
                    if (!string.IsNullOrEmpty(symbol))
                    {
                        UpdateCache(data);
                    }
                }
            }

            return await Task.FromResult(symbol);
        }

        /// <summary>
        ///     Получить метаданные по инструменту
        /// </summary>
        /// <param name="instrument">
        /// </param>
        /// <returns>
        ///     Асихронный результат с метаданными инструмента
        /// </returns>
        public async Task<TData> ResolveInstrumentAsync(Instrument instrument)
        {
            TData data = GetDataByInstrument(instrument);

            if (data.Equals(default(TData)))
            {
                data = await (this as IInstrumentConverter<TData>).ResolveInstrumentAsync(instrument);

                if (!data.Equals(default(TData)))
                {
                    UpdateCache(data);
                }
            }

            return await Task.FromResult(data);
        }

        /// <summary>
        ///     Получить инструмент для конкретного транспорта
        /// </summary>
        /// <param name="instrument">
        ///     Внешний по отношению к конкретному транспорту инструмент
        /// </param>
        /// <returns>
        ///     Возвращает инструмент в нотации конкретного транспорта
        /// </returns>
        public async Task<Instrument> ResolveTransportInstrumentAsync(Instrument instrument)
        {
            Instrument transportInstrument = GetTransportInstrumentByExternalInstrument(instrument);
            if (transportInstrument == default(Instrument))
            {
                TData data = await (this as IInstrumentConverter<TData>).ResolveInstrumentAsync(instrument);

                if (!data.Equals(default(TData)))
                {
                    transportInstrument = data.TransportInstrument;
                    UpdateCache(data);
                    UpdateCache(instrument, transportInstrument);
                }
            }

            return await Task.FromResult(transportInstrument);
        }

        #endregion

        #region IInstrumentConverter

        /// <summary>
        ///     Разрешить инструмент по вендорскому коду
        /// </summary>
        /// <param name="symbol">
        ///     Вендорский код
        /// </param>
        /// <param name="dependentObjectDescription">
        ///     Строковое описание объекта, который зависит от инструмента
        /// </param>
        /// <returns>
        ///     Асихронный результат с инструментом
        /// </returns>
        async Task<TData> IInstrumentConverter<TData>.ResolveSymbolAsync(string symbol, string dependentObjectDescription = null)
        {
            if (_externalConverter == null)
            {
                var data = Activator.CreateInstance<TData>();
                data.Symbol = symbol;
                data.TransportInstrument = new Instrument {Code = symbol}; 

                return await Task.FromResult(data);
            }

            return await _externalConverter.ResolveSymbolAsync(symbol, dependentObjectDescription);
        }

        /// <summary>
        ///     Получить символ по инструменту
        /// </summary>
        /// <param name="instrument"> </param>
        /// <returns>
        ///     Асихронный результат с вендорским кодом инструмента
        /// </returns>
        async Task<TData> IInstrumentConverter<TData>.ResolveInstrumentAsync(Instrument instrument)
        {
            if (_externalConverter == null)
            {
                var data = Activator.CreateInstance<TData>();
                data.Symbol = instrument.Code;
                data.TransportInstrument = instrument;

                return await Task.FromResult(data);
            }

            return await _externalConverter.ResolveInstrumentAsync(instrument);
        }

        #endregion
        
        #region ConverterCache

        /// <summary>
        ///     Кэш для маппинга внешнего инструмента в инструмент для конкретного транспорта
        /// </summary>
        private sealed class CacheTransportInstrumentByExternalInstrument : ConverterCache<Instrument, Instrument> { }
        
        /// <summary>
        ///     Кэш для маппинга инструмента в метаданные по инструменту 
        /// </summary>
        private sealed class CacheDataByInstrument : ConverterCache<Instrument, TData> { }

        /// <summary>
        ///     Кэш для маппинга символа в инструмент
        /// </summary>
        private sealed class CacheInstrumentBySymbol : ConverterCache<string, Instrument> { }

        /// <summary>
        ///     Кэш для маппинга инструмента в символ
        /// </summary>
        private sealed class CacheSymbolByInstrument : ConverterCache<Instrument, string> { } 

        #endregion
    }
}
