using JetBrains.Annotations;

namespace Polygon.Messages
{
    /// <summary>
    ///     Обработчик сообщений, реализующий паттерн Visitor.
    /// </summary>
    [PublicAPI]
    public interface IMessageVisitor
    {
        /// <summary>
        ///     Обработка сообщения <see cref="InitResponseMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(InitResponseMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="FillMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(FillMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="PositionMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(PositionMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="MoneyPosition"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(MoneyPosition message);

        /// <summary>
        ///     Обработка сообщения <see cref="InstrumentParams"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(InstrumentParams message);

        /// <summary>
        ///     Обработка сообщения <see cref="SessionInfo"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(SessionInfo message);

        /// <summary>
        ///     Обработка сообщения <see cref="OrderBook"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(OrderBook message);

        /// <summary>
        ///     Обработка сообщения <see cref="OrderStateChangeMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(OrderStateChangeMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="ExternalOrderMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(ExternalOrderMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="ErrorMessage"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(ErrorMessage message);

        /// <summary>
        ///     Обработка сообщения <see cref="ServerTimeInfo"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(ServerTimeInfo message);

        /// <summary>
        ///     Обработка сообщения <see cref="MarketModeInfo"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(MarketModeInfo message);

        /// <summary>
        ///     Обработка сообщения <see cref="TransactionReply"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(TransactionReply message);

        /// <summary>
        ///     Обработка сообщения <see cref="Trade"/>.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(Trade message);

        /// <summary>
        ///     Обработка прочих сообщений.
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки.
        /// </param>
        void Visit(Message message);
    }
}

