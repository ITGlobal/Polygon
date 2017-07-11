using System.Threading.Tasks;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Предназначается для источников данных, которые требуют подписки на инструменты.
    /// </summary>
    [PublicAPI]
    public interface IInstrumentParamsSubscriber
    {
        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        Task<SubscriptionResult> Subscribe([NotNull] Instrument instrument);

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        void Unsubscribe([NotNull] Instrument instrument);
    }
}

