using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Провайдер исторических данных
    /// </summary>
    [PublicAPI]
    public interface IInstrumentHistoryProvider
    {
        /// <summary>
        ///     Определить оптимальную длину блока для запроса исторических данных
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        /// <returns>
        ///     Оптимальная длина блока либо null
        /// </returns>
        TimeSpan? GetBestFetchBlockLength([NotNull] Instrument instrument, HistoryProviderSpan span);

        /// <summary>
        ///     Получить исторические данные
        /// </summary>
        /// <param name="consumer">
        ///     Потребитель исторических данных
        /// </param>
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
        /// <param name="cancellationToken">
        ///     Токен отмены
        /// </param>
        /// <returns>
        ///     Исторические данные
        /// </returns>
        /// <remarks>
        ///     Провайдер вправе переопределить параметры исторических графиков - диапазон, интервал,
        ///     если он не в состоянии предоставить запрошенные данные.
        /// </remarks>
        /// <exception cref="NoHistoryDataException">
        ///     Бросается, если исторические данные за указанный период недоступны
        /// </exception>
        [NotNull]
        Task GetHistoryDataAsync(
            [NotNull] IHistoryDataConsumer consumer,
            [NotNull] Instrument instrument,
            DateTime begin, 
            DateTime end,
            HistoryProviderSpan span,
            CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        ///     Подписаться на исторические данные
        /// </summary>
        /// <param name="consumer">
        ///     Потребитель исторических данных
        /// </param>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="begin">
        ///     Начало диапазона
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        /// <returns>
        ///     Подписка на исторические данные
        /// </returns>
        /// <remarks>
        ///     Провайдер вправе переопределить параметры исторических графиков - диапазон, интервал,
        ///     если он не в состоянии предоставить запрошенные данные.
        /// </remarks>
        [NotNull]
        Task<IHistoryDataSubscription> SubscribeToHistoryDataAsync(
            [NotNull] IHistoryDataConsumer consumer,
            [NotNull] Instrument instrument,
            DateTime begin,
            HistoryProviderSpan span);
    }
}

