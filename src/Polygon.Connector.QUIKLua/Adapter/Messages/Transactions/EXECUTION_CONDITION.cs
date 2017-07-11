namespace Polygon.Connector.QUIKLua.Adapter.Messages.Transactions
{
    /// <summary>
    /// Условие исполнения заявки, необязательный параметр.
    /// </summary>
    internal enum EXECUTION_CONDITION
    {
        /// <summary>
        /// поставить в очередь (по умолчанию),
        /// </summary>
        PUT_IN_QUEUE,

        /// <summary>
        /// немедленно или отклонить,
        /// </summary>
        FILL_OR_KILL,

        /// <summary>
        /// снять остаток
        /// </summary>
        KILL_BALANCE

    }
}

