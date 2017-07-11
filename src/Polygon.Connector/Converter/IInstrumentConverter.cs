using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс конвертера инструментов
    /// </summary>
    public interface IInstrumentConverter<T> where T : InstrumentData
    {
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
        ///     Асихронный результат с метаданными по инструменту
        /// </returns>
        Task<T> ResolveSymbolAsync(string symbol, string dependentObjectDescription = null);

        /// <summary>
        ///     Разрешить символ по инструменту
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns>
        ///     Асихронный результат с метаданными по инструменту
        /// </returns>
        Task<T> ResolveInstrumentAsync(Instrument instrument);
    }
}
