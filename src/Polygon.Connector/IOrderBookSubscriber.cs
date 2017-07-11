using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Предназначается для источников данных, которые требуют подписки на стаканы.
    /// </summary>
    [PublicAPI]
    public interface IOrderBookSubscriber
    {
        /// <summary>
        ///     Подписаться на стакан по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        void SubscribeOrderBook([NotNull] Instrument instrument);

        /// <summary>
        ///     Отписаться от стакана по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        void UnsubscribeOrderBook([NotNull] Instrument instrument);
    }
}

