using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// Обёртка над множеством словарей словарей словарей, с которыми работает раутер
    /// </summary>
    internal sealed class OrderRouterContainer
    {
        #region Fields
        /// <summary>
        /// коллекция с заявками по id заявок, тут лежат все заявки
        /// </summary>
        private readonly Dictionary<string, Order> mapOrderId2Order = new Dictionary<string, Order>();

        /// <summary>
        /// коллекция с заявками по extRef, сюда кладём заявки при выставлении (пока не знаем id)
        /// </summary>
        private readonly Dictionary<long, Order> mapOrderExtId2Order = new Dictionary<long, Order>();

        /// <summary>
        /// Заявки, по которым уже приходили какие-либо статусы
        /// </summary>
        private readonly HashSet<string> activatedOrders = new HashSet<string>();

        /// <summary>
        /// сюда складывать сделки, которые пришли до каких либо эвентов по заявкам
        /// как только по заявке пришло что-то, тут же эти сделки отправляем клиентам
        /// </summary>
        private readonly Dictionary<string, LinkedList<FillMessage>> pendingFills = new Dictionary<string, LinkedList<FillMessage>>();

        /// <summary>
        /// коллекция с ответами на транзакции, ожидающими проходжа всех своих стейтов сначала
        /// </summary>
        private readonly Dictionary<string, TransactionReplyHelper> pendingReplies = new Dictionary<string, TransactionReplyHelper>();

        /// <summary>
        /// коллекция с транзакциями, на которые мы ждём ответ
        /// </summary>
        private readonly Dictionary<long, Transaction> transactions = new Dictionary<long, Transaction>();

        #endregion

        #region Транзакции

        /// <summary>
        /// Сохранить транзакцию по целочисленному id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        public void AddTransaction(uint id, Transaction transaction)
        {
            transactions.Add(id, transaction);
        }

        /// <summary>
        /// Сохранить транзакцию по целочисленному id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        public void AddTransaction(int id, Transaction transaction)
        {
            transactions.Add(id, transaction);
        }

        /// <summary>
        /// Получить транзакцию по id и удалить её из контэйнера
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool RemoveTransaction(uint id, out Transaction transaction)
        {
            return transactions.TryGetValue(id, out transaction);
        }

        #endregion

        #region Хранение заявки по биржевому номеру

        /// <summary>
        /// Добавить заявки по биржевому номеру (сконвертированному в строку)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        public void AddOrder(string id, Order order)
        {
            mapOrderId2Order.Add(id, order);
        }

        /// <summary>
        /// Получить заявку по биржевому номеру (сконвертированному в строку)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool TryGetOrder(string id, out Order order)
        {
            return mapOrderId2Order.TryGetValue(id, out order);
        }

        /// <summary>
        /// Обновить заявку по биржевому номеру
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        public void UpdateOrder(string id, Order order)
        {
            mapOrderId2Order[id] = order;
        }

        #endregion

        #region Pending replies

        public void AddPendingReply(string id, TransactionReplyHelper replyHelper)
        {
            pendingReplies.Add(id, replyHelper);
        }

        public bool TryRemovePendingReply(string id, out TransactionReplyHelper replyHelper)
        {
            var rValue = pendingReplies.TryGetValue(id, out replyHelper);

            if (rValue)
                pendingReplies.Remove(id);

            return rValue;
        }

        #endregion

        #region Хранение заявки по идентификатору транзакции

        /// <summary>
        /// Сохранить связку идентификатор транзакции - заявка
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        public void AddOrder(uint id, Order order)
        {
            mapOrderExtId2Order.Add(id, order);
        }

        /// <summary>
        /// Получить заявку по идентификатору транзакции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool TryGetOrder(uint id, out Order order)
        {
            return mapOrderExtId2Order.TryGetValue(id, out order);
        }

        /// <summary>
        /// Получить и удалить заявку по идентификатору транзакции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool TryRemoveOrder(uint id, out Order order)
        {
            var rValue = mapOrderExtId2Order.TryGetValue(id, out order);

            if (rValue)
            {
                mapOrderExtId2Order.Remove(id);
            }

            return rValue;
        }

        #endregion

        #region Заявки, принятые системой

        /// <summary>
        /// Добавить активированную заявку (её биржевой номер)
        /// </summary>
        /// <param name="id"></param>
        public void AddActivatedOrder(string id)
        {
            if (!activatedOrders.Contains(id))
            {
                activatedOrders.Add(id);
            }
        }

        /// <summary>
        /// Проверить, была ли активирована заявка с указанным биржевым номером
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsOrderActivated(string id)
        {
            return activatedOrders.Contains(id);
        }

        #endregion

        #region Сделки, ожидающие обработки (не пришёл статус заявки)

        /// <summary>
        /// Добавить сделки, ожидающие прихода статуса заявки с биржевым номером id
        /// </summary>
        /// <param name="id">Биржевой номер заявки, к которой относятся сделки</param>
        /// <param name="fills">Сделки, которым ожидают прихода статуса заявки</param>
        public void AddPendingFills(string id, LinkedList<FillMessage> fills)
        {
            pendingFills.Add(id, fills);
        }

        /// <summary>
        /// Получить сделки, ожидающие прихода статуса по заявке с биржевым номером orderExchangeId
        /// </summary>
        /// <param name="orderExchangeId">Биржевой номер заявки, для которой следует вернуть ожидающие сделки</param>
        /// <param name="fills">Возвращаемое значение - набор сделок</param>
        /// <returns></returns>
        public bool TryGetPendingFill(string orderExchangeId, out LinkedList<FillMessage> fills)
        {
            return pendingFills.TryGetValue(orderExchangeId, out fills);
        }

        /// <summary>
        /// Получить и удалить сделки, ожидающие прихода статуса по заявке с биржевым номером orderExchangeId
        /// </summary>
        /// <param name="orderExchangeId">Биржевой номер заявки, для которой следует вернуть ожидающие сделки</param>
        /// <param name="fills">Возвращаемое значение - набор сделок</param>
        public bool TryRemovePendingFill(string orderExchangeId, out LinkedList<FillMessage> fills)
        {
            var rValue = pendingFills.TryGetValue(orderExchangeId, out fills);

            if (rValue)
            {
                pendingFills.Remove(orderExchangeId);
            }

            return rValue;
        }

        #endregion

    }

}

