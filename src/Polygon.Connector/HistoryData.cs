using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Исторические данные
    /// </summary>
    [PublicAPI]
    public sealed class HistoryData
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="begin">
        ///     Начало диапазона
        /// </param>
        /// <param name="end">
        ///     Конец диапазона
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        public HistoryData(
            [NotNull] Instrument instrument,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span)
        {
            Instrument = instrument;
            Begin = begin;
            End = end;
            Span = span;
        }

        /// <summary>
        ///     Инструмент
        /// </summary>
        [NotNull]
        [JsonIgnore]
        public Instrument Instrument { get; }
        
        /// <summary>
        ///     Начало диапазона
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        ///     Конец диапазона
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        ///     Интервал свечей для исторических данных
        /// </summary>
        public HistoryProviderSpan Span { get; set; }

        /// <summary>
        ///     Точки данных
        /// </summary>
        [NotNull]
        public IList<HistoryDataPoint> Points { get; } = new List<HistoryDataPoint>();
    }
}

