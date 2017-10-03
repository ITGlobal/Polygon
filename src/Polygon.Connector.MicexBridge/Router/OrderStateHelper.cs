namespace Polygon.Connector.MicexBridge.Router
{
    class OrderStateHelper : Message
    {
        #region Properties

        /// <summary>
        /// Оставшееся не исполненое количество.
        /// </summary>
        public uint ActiveQty { get; set; }


        /// <summary>
        /// Сейчас это поле заполняется только если State == OrderState.Error и содержит
        /// доп. информацию об ошибке.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public long OrderExchangeId { get; set; }


        /// <summary>
        /// Текущий статус заявки.
        /// </summary>
        public OrderState State { get; set; }


        /// <summary>
        /// Комментарий, для идентификации заявки
        /// </summary>
        public string ExtRef { get; set; }

        /// <summary>
        /// TransactionId, собственно
        /// если тут 0, смотрится ExtRef
        /// </summary>
        public uint TransactionId;

        #endregion
    }
}