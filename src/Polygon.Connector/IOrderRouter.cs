using System.Collections.Generic;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс типичного диспетчера заявок.
    /// </summary>
    [PublicAPI]
    public interface IOrderRouter : IGatewayService
    {
        /// <summary>
        ///     Список доступных для торговли счетов.
        /// </summary>
        List<string> AvailableAccounts { get; }

        /// <summary>
        ///     Доступна ли работа по данному счёту
        /// </summary>
        bool IsPermittedAccount(string account);

        /// <summary>
        ///     Отправляет заявки.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        void SendTransaction(Transaction transaction);

        ///// <summary>
        /////     Добавляет подписку на заданный тип сообщения по заданному счету.
        ///// </summary>
        ///// <param name="account">
        /////     Номер счёта.
        ///// </param>
        ///// <param name="subscriptionType">
        /////     Тип сообщения.
        ///// </param>
        //void RequestSessionData(string account, SubscriptionType subscriptionType);
    }
}

