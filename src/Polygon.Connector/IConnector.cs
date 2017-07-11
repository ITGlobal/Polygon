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
        ///     Провайдер исторических данных
        /// </summary>
        [CanBeNull]
        IInstrumentHistoryProvider HistoryProvider { get; }

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

