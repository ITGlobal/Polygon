using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Потребитель исторических данных
    /// </summary>
    [PublicAPI]
    public interface IHistoryDataConsumer
    {
        /// <summary>
        ///     Обновить исторические данные
        /// </summary>
        void Update([NotNull] HistoryData data, HistoryDataUpdateType updateType);

        /// <summary>
        ///     Обработать критическую ошибку исторических данных
        /// </summary>
        void Error([NotNull] string message);

        /// <summary>
        ///     Обработать некритическую ошибку исторических данных
        /// </summary>
        void Warning([NotNull] string message);
    }
}

