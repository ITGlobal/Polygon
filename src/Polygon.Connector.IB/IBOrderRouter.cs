using Polygon.Connector;
using Polygon.Connector.InteractiveBrokers;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Раутер для IB
    /// </summary>
    internal sealed class IBOrderRouter : OrderRouter
    {
        private readonly IBConnector connector;
        private readonly TransactionDispatcher transactionDispatcher;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBOrderRouter(IBConnector connector, string sessionUid, OrderRouterMode mode)
            : base(sessionUid: sessionUid, mode: mode)
        {
            this.connector = connector;
            transactionDispatcher = new TransactionDispatcher(connector);
        }

        /// <summary>
        ///   Запускает сервис.
        /// </summary>
        public override void Start()
        { }

        /// <summary>
        ///   Останавливает сервис.
        /// </summary>
        public override void Stop()
        { }

        /// <summary>
        ///     Отправляет заявки на биржу.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        protected override void SendTransactionImp(Transaction transaction)
        {
            transactionDispatcher.Send(transaction);
        }
        
        internal string SessionUidInternal => SessionUid;
        internal OrderRouterMode ModeInternal => Mode;

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(MoneyPosition message)
        {
            OnMessageReceived(message);
        }

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(PositionMessage message)
        {
            OnMessageReceived(message);
        }

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(FillMessage message)
        {
            OnMessageReceived(message);
        }

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(TransactionReply message)
        {
            OnMessageReceived(message);
        }

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(ExternalOrderMessage message)
        {
            OnMessageReceived(message);
        }

        /// <summary>
        ///     Выплюнуть из раутера сообщение
        /// </summary>
        /// <param name="message">
        ///     Сообщение
        /// </param>
        internal void Transmit(OrderStateChangeMessage message)
        {
            OnMessageReceived(message);
        }

        internal bool FilterByComment(string comment)
        {
            var isCommentChecked = false;
            if (CheckComment && comment != null)
            {
                if (comment.Length >= CommentLength)
                {
                    var customComment = comment.Substring(comment.Length - CommentLength, CommentLength);
                    isCommentChecked = customComment.StartsWith(SessionUid);
                }
                else if (comment.Length >= SessionUidLength)
                {
                    isCommentChecked = comment.EndsWith(SessionUid);
                }
            }

            return !CheckComment || isCommentChecked;
        }
    }
}

