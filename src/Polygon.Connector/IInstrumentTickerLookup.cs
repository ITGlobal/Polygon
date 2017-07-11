using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс для поиска инструментов по коду
    /// </summary>
    [PublicAPI]
    public interface IInstrumentTickerLookup
    {
        /// <summary>
        ///     Поиск тикеров по (частичному) коду
        /// </summary>
        string[] LookupInstruments(string code, int maxResults = 10);
    }
}