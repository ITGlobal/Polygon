namespace Polygon.Connector.QUIKLua.Adapter.Messages.Transactions
{
    internal enum ACTION
    {
        /// <summary>
        /// новая заявка,
        /// </summary>
        NEW_ORDER,

        /// <summary>
        ///  новая заявка на внебиржевую сделку
        /// </summary>
        NEW_NEG_DEAL,

        /// <summary>
        /// новая заявка на сделку РЕПО
        /// </summary>
        NEW_REPO_NEG_DEAL,

        /// <summary>
        /// новая заявка на сделку модифицированного РЕПО (РЕПО,М)
        /// </summary>
        NEW_EXT_REPO_NEG_DEAL,

        /// <summary>
        ///  новая стоп,заявка
        /// </summary>
        NEW_STOP_ORDER,

        /// <summary>
        /// снять заявку
        /// </summary>
        KILL_ORDER,

        /// <summary>
        /// снять заявку на внебиржевую сделку или заявку на сделку РЕПО
        /// </summary>
        KILL_NEG_DEAL,

        /// <summary>
        ///  снять стоп,заявку
        /// </summary>
        KILL_STOP_ORDER,

        /// <summary>
        /// снять все заявки из торговой системы
        /// </summary>
        KILL_ALL_ORDERS,

        /// <summary>
        /// снять все стоп,заявки
        /// </summary>
        KILL_ALL_STOP_ORDERS,

        /// <summary>
        /// снять все заявки на внебиржевые сделки и заявки на сделки РЕПО
        /// </summary>
        KILL_ALL_NEG_DEALS,

        /// <summary>
        /// снять все заявки на рынке FORTS
        /// </summary>
        KILL_ALL_FUTURES_ORDERS,

        /// <summary>
        /// удалить лимит открытых позиций на спот,рынке RTS Standard
        /// </summary>
        KILL_RTS_T4_LONG_LIMIT,

        /// <summary>
        /// удалить лимит открытых позиций клиента по спот,активу на рынке RTS Standard
        /// </summary>
        KILL_RTS_T4_SHORT_LIMIT,

        /// <summary>
        /// переставить заявки на рынке FORTS
        /// </summary>
        MOVE_ORDERS,

        /// <summary>
        /// новая безадресная заявка
        /// </summary>
        NEW_QUOTE,

        /// <summary>
        /// снять безадресную заявку
        /// </summary>
        KILL_QUOTE,

        /// <summary>
        /// новая  заявка,отчет о подтверждении транзакций в режимах РПС и РЕПО
        /// </summary>
        NEW_REPORT,

        /// <summary>
        /// новое ограничение по фьючерсному счету
        /// </summary>
        SET_FUT_LIMIT

    }
}


