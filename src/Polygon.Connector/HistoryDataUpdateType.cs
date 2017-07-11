using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Тип обновления исторических данных
    /// </summary>
    [PublicAPI]
    public enum HistoryDataUpdateType
    {
        /// <summary>
        ///     Пачка точек
        /// </summary>
        Batch,

        /// <summary>
        ///     Добавлена одна последняя точка
        /// </summary>
        OnePointAdded,

        /// <summary>
        ///     Обновлена одна последняя точка
        /// </summary>
        OnePointUpdated,
    }
}

