using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Фасад транспорта
    /// </summary>
    [PublicAPI]
    public interface IConnector : IDisposable
    {
        /// <summary>
        ///     Название транспорта
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        [CanBeNull]
        IFeed Feed { get; }

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        [CanBeNull]
        IOrderRouter Router { get; }

        /// <summary>
        ///     Подписчик на параметры инструментов
        /// </summary>
        [CanBeNull]
        IInstrumentParamsSubscriber InstrumentParamsSubscriber { get; }

        /// <summary>
        ///     Подписчик на стаканы по инструментам
        /// </summary>
        [CanBeNull]
        IOrderBookSubscriber OrderBookSubscriber { get; }

        /// <summary>
        ///     Поиск инструментов по коду
        /// </summary>
        [CanBeNull]
        IInstrumentTickerLookup InstrumentTickerLookup { get; }

        /// <summary>
        ///     Провайдер кодов инструментов для FORTS
        /// </summary>
        [CanBeNull]
        IFortsDataProvider FortsDataProvider { get; }

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        [CanBeNull]
        IInstrumentHistoryProvider HistoryProvider { get; }

        /// <summary>
        ///     Провайдеры статусов соединений
        /// </summary>
        [NotNull]
        IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        void Start();

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        void Stop();

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool SupportsOrderModification([NotNull] string account);
    }
}

