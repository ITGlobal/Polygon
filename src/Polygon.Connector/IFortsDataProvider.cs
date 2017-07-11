using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Провайдер кодов инструментов для FORTS
    /// </summary>
    [PublicAPI]
    public interface IFortsDataProvider
    {
        /// <summary>
        ///     Получить FullCode для инструмента
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <returns>
        ///     FullCode либо null
        /// </returns>
        [CanBeNull]
        string QueryFullCode([NotNull] Instrument instrument);
    }
}

