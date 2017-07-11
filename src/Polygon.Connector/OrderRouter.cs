using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Базовый класс типичного раутера.
    /// </summary>
    [PublicAPI]
    public abstract class OrderRouter : GatewayService, IOrderRouter
    {
        /// <summary>
        ///     Сохранять ли все собственные сделки в памяти, для возможности отдать их по запросу.
        /// </summary>
        private readonly bool storeFillsInMemory = true;
        private readonly HashSet<string> mapPermittedAccounts;

        /// <summary>
        ///     Создает экземпляр диспетчера заявок.
        /// </summary>
        protected OrderRouter(
            bool storeFillsInMemory = true,
            IEnumerable<string> permittedAccounts = null,
            string sessionUid = null,
            OrderRouterMode mode = OrderRouterMode.ThisSessionOnly)
        {
            SessionUid = (sessionUid ?? Guid.NewGuid().ToString()).Substring(0, SessionUidLength);
            Mode = mode;

            AvailableAccounts = new List<string>();
            Portfolios = new Dictionary<string, Dictionary<Instrument, PositionMessage>>();
            Limits = new Dictionary<string, MoneyPosition>();
            Fills = new Dictionary<string, Dictionary<Instrument, List<FillMessage>>>();

            this.storeFillsInMemory = storeFillsInMemory;

            mapPermittedAccounts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (permittedAccounts != null)
            {
                mapPermittedAccounts.UnionWith(permittedAccounts);
            }
        }

        protected const int SessionUidLength = 2;
        protected const int CommentLength = 5;

        protected string SessionUid { get; private set; }

        protected OrderRouterMode Mode { get; private set; }

        protected bool ReceiveExternalOrders { get { return Mode != OrderRouterMode.ThisSessionOnly; } }

        protected bool CheckComment { get { return Mode != OrderRouterMode.ExternalSessionsRenewable; } }

        /// <summary>
        ///     Список доступных для торговли счетов.
        /// </summary>
        public List<string> AvailableAccounts { get; protected set; }

        /// <summary>
        ///     Коллекция филов (собственных сделок).
        /// </summary>
        public Dictionary<string, Dictionary<Instrument, List<FillMessage>>> Fills { get; protected set; }

        /// <summary>
        ///     Портфели по счетам.
        /// </summary>
        protected Dictionary<string, Dictionary<Instrument, PositionMessage>> Portfolios { get; set; }

        /// <summary>
        ///     Ограничения по счетам.
        /// </summary>
        protected Dictionary<string, MoneyPosition> Limits { get; set; }

        /// <summary>
        ///     Доступна ли работа по данному счёту
        /// </summary>
        public bool IsPermittedAccount(string account)
        {
            return mapPermittedAccounts.Count == 0 || mapPermittedAccounts.Contains(account);
        }

        /// <summary>
        ///     Отправляет заявки.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        public void SendTransaction(Transaction transaction)
        {
            if (IsPermittedAccount(transaction.Account))
            {
                SendTransactionImp(transaction);
            }
            else
            {
                OnMessageReceived(new ErrorMessage
                {
                    Message = $"Order router settings forbid placing order on account \"{transaction.Account}\""
                });
            }
        }

        /// <summary>
        ///     Отправляет заявки на биржу.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        protected abstract void SendTransactionImp(Transaction transaction);

        /// <summary>
        ///     Добавляет подписку на заданный тип сообщения по заданному счету.
        /// </summary>
        /// <param name="account">
        ///     Номер счёта.
        /// </param>
        /// <param name="subscriptionType">
        ///     Тип сообщения.
        /// </param>
        public void RequestSessionData(string account, SubscriptionType subscriptionType)
        {
            switch (subscriptionType)
            {
                case SubscriptionType.MoneyPosition:
                    if (Limits.ContainsKey(account))
                    {
                        OnMessageReceived(Limits[account]);
                    }

                    break;
                case SubscriptionType.Position:
                    if (Portfolios.ContainsKey(account))
                    {
                        foreach (var position in Portfolios[account].Values)
                        {
                            OnMessageReceived(position);
                        }
                    }

                    break;
                case SubscriptionType.Fill:
                    if (Fills.ContainsKey(account))
                    {
                        foreach (var fill in Fills[account].Values.SelectMany(list => list))
                        {
                            OnMessageReceived(fill);
                        }
                    }

                    break;
                default:
                    Logger.Warn().Print($"Requested session data on account {account} of type {subscriptionType}.");
                    break;
            }
        }

        protected bool AddAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
                return false;

            if (!IsPermittedAccount(account))
                return false;

            using (SyncRoot.Lock())
            {
                if (AvailableAccounts.Contains(account))
                {
                    return false;
                }

                AvailableAccounts.Add(account);
                Fills.Add(account, new Dictionary<Instrument, List<FillMessage>>());
                return true;
            }
        }

        /// <summary>
        /// Сохраняет свою сделку.
        /// </summary>
        /// <param name="fill">Данные своей сделки.</param>
        protected void AddFill(FillMessage fill)
        {
            if (!storeFillsInMemory)
                return;

            if (!IsPermittedAccount(fill.Account))
                return;

            using (SyncRoot.Lock())
            {
                Dictionary<Instrument, List<FillMessage>> accountFills;

                if (!AvailableAccounts.Contains(fill.Account))
                {
                    AvailableAccounts.Add(fill.Account);
                    accountFills = new Dictionary<Instrument, List<FillMessage>>();
                    Fills.Add(fill.Account, accountFills);
                }
                else
                {
                    accountFills = Fills[fill.Account];
                }

                List<FillMessage> instrumentFills;

                if (!accountFills.TryGetValue(fill.Instrument, out instrumentFills))
                {
                    instrumentFills = new List<FillMessage>();
                    accountFills.Add(fill.Instrument, instrumentFills);
                }

                instrumentFills.Add(fill);
            }
        }
    }
}

