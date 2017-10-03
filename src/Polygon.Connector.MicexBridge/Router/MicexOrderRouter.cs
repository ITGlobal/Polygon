using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
    public class MicexOrderRouter : OrderRouter
    {
        private readonly string connectionStringForDataUpdates;
        private readonly string loginForDataUpdates;
        private readonly int transactionsRefreshementTimeout;

        
		/// <summary>
		/// Интерфейс логгера.
		/// </summary>
		private readonly ILog logger;


        /// <summary>
        /// Переметры подключения к шлюзу.
        /// </summary>
        private readonly ConnectionParameters connectionParamsForTransactions;

        /// <summary>
        /// Логин с которым происходит подключение к шлюзу для отправки транзакций.
        /// </summary>
        private readonly string loginForTransactions;


        /// <summary>
        /// Идентификатор данной сесии, в пределах которой уникальны все transactionId присваиваемые ордарам.
        /// </summary>
        private readonly string sessionId;

        /// <summary>
        /// Длинна идентификатора сессии.
        /// </summary>
        private const int SessionIdLength = 5;



        /// <summary>
        /// Обертка над mtesrl.dll которую мы используем для отпарвки транзакций в шлюз.
        /// </summary>
        private MtesrlWrapper apiWrapperForTransactions;


        /// <summary>
        /// Набор таблиц доступных в данном интерфейсе шлюза.
        /// </summary>
        private TableType[] tableTypes;


        /// <summary>
        /// Типы транзакций в данном интерфейсе шлюза.
        /// </summary>
        private TransactionType[] transactionTypes;

        /// <summary>
        /// Секция ММВБ.
        /// </summary>
        private readonly MicexSecionType micexSectionType;



        /// <summary>
        /// Адаптер к данным раутинга для конкретной секции ММВБ.
        /// </summary>
        private MicexSectionOrderRouterAdapter sectionOrderRouterAdapter;


        /// <summary>
        /// Номер транзакции, который был присвоен последней транзакции. Следующей транзакции
        /// присваивается ++transactionId
        /// </summary>
        private uint transactionId;


        private OrderStateProvider orderStateProvider;


        public MicexOrderRouter(
			MicexSecionType sectionType,
			string connectionStringForTransactions,
			string loginForTransactions,
			string connectionStringForDataUpdates,
			string loginForDataUpdates,
			int transactionsRefreshementTimeout,
            bool doSaveFills=true,
            string[] permittedAccounts = null)
            : base(doSaveFills, permittedAccounts)
        {
            micexSectionType = sectionType;
			logger = LogManager.GetLogger(GetType());

            sessionId = Guid.NewGuid().ToString().Substring(0, SessionIdLength);


			allOrders = new Dictionary<uint, Order>();
            exchangeIdToTransactId = new Dictionary<long, uint>();

			sessionId = Guid.NewGuid().ToString().Substring(0, SessionIdLength);

			this.loginForTransactions = loginForTransactions;
            this.connectionStringForDataUpdates = connectionStringForDataUpdates;
            this.loginForDataUpdates = loginForDataUpdates;
            this.transactionsRefreshementTimeout = transactionsRefreshementTimeout;

            connectionParamsForTransactions = new ConnectionParameters(connectionStringForTransactions);

            logger.DebugFormat("Создан MicexOrderRouter для секции {0}. Идентификатор сессии {1}", sectionType, sessionId);
		}

        /// <summary>
        /// Список всех транзакций прошедших через раутер. Ключ - id транзакции.
        /// </summary>
        private readonly Dictionary<uint, Order> allOrders;

        private readonly Dictionary<long, uint> exchangeIdToTransactId;

        protected override void SendTransactionToExchange(Transaction transaction)
        {
            //if (token.IsCancellationRequested)
            //{
            //    logger.ErrorFormat("Цикл обновления статусов транзакций остановлен. Заявка {0} отправлена не будет.", order.OrderId);
            //    return;
            //}


            logger.InfoFormat(
                "Заявка {0}; {1}; ",
                transaction.TransactionId,
                transaction.Instrument.Code);

            var decimals = orderStateProvider.GetDecimalsForInstrument(transaction.Instrument);
            var res = string.Empty;
            var error = false;

            lock (apiWrapperForTransactions)
            {
                uint rest;
                switch (transaction.Action)
                {
                    case OrderAction.NewOrder:
                        var order = new Order((NewOrderTransaction)transaction);
                        allOrders.Add(++transactionId, order);

                        error =
                            !apiWrapperForTransactions.ExecTrans(
                                transactionTypes[sectionOrderRouterAdapter.SendOrderTransactionIndex].Name,
                                sectionOrderRouterAdapter.GetParamsForSendOrder(
                                    transactionTypes[sectionOrderRouterAdapter.SendOrderTransactionIndex].Input,
                                    (NewOrderTransaction)transaction,
                                    sessionId,
                                    transactionId,
                                    (int)decimals),
                                out res);

                        logger.InfoFormat("Отправка заявки {0}. {1}: {2}", order.TransactionId, error ? "Ошибка" : "Результат", res);

                        if (!error)
                        {
                            // в случае успеха, парсим ответ транзакции
                            // получаем id заявки и кол-во уже исполненного
                            long id;
                            uint filled = ParseOrderReply(res, out id);

                            //добавляем связь id - transactionId в словарь
                            exchangeIdToTransactId.Add(id, transactionId);

                            rest = order.Quantity - filled;

                            // тут собираем сообщение о стейте, как будто бы оно пришло из OrderStateProvider
                            OrderStateProviderMessageReceived(new OrderStateHelper
                                                                  {
                                                                      ActiveQty = rest,
                                                                      OrderExchangeId = id,
                                                                      TransactionId = transactionId,
                                                                      State = rest == 0 ? OrderState.Filled : OrderState.Active,
                                                                  });
                        }

                        OnMessageReceived(new TransactionReply
                        {
                            Message = res,
                            Client = transaction.Client,
                            TransactionId = transaction.TransactionId,
                            Success = !error
                        });


                        break;

                    case OrderAction.KillOrder:

                        KillOrderTransaction killOrderTransaction = (KillOrderTransaction)transaction;

                        error =
                            !apiWrapperForTransactions.ExecTrans(
                                transactionTypes[sectionOrderRouterAdapter.DelOrderByIdTransactionIndex].Name,
                                sectionOrderRouterAdapter.GetParamsForDelByOrderIdOrder(
                                    transactionTypes[sectionOrderRouterAdapter.DelOrderByIdTransactionIndex].Input, killOrderTransaction.OrderExchangeId),
                                out res);

                        logger.InfoFormat("Удаление заявки {0}. {1}: {2}", transaction.TransactionId, error ? "Ошибка" : "Результат", res);

                        if (!error)
                        {
                            // в случае успеха, парсим ответ транзакции
                            // получаем снятое кол-во в заявке
                            rest = ParseCancelReply(res);

                            //получаем TransactId из словаря, это надо для последующей идентификации заявки
                            uint tId;
                            if (!exchangeIdToTransactId.TryGetValue(killOrderTransaction.OrderExchangeId, out tId))
                                tId = 0;
                            else
                                exchangeIdToTransactId.Remove(killOrderTransaction.OrderExchangeId);

                            // тут собираем сообщение о стейте, как будто бы оно пришло из OrderStateProvider
                            OrderStateProviderMessageReceived(new OrderStateHelper
                                                                  {
                                                                      ActiveQty = rest,
                                                                      OrderExchangeId = killOrderTransaction.OrderExchangeId,
                                                                      TransactionId = tId,
                                                                      State = OrderState.Cancelled,
                                                                  });
                        }

                        OnMessageReceived(new TransactionReply
                                              {
                                                  Message = res,
                                                  Client = killOrderTransaction.Client,
                                                  TransactionId = killOrderTransaction.TransactionId,
                                                  Success = !error
                                              });


                        break;
                    default:
                        OnMessageReceived(new ErrorMessage
                                              {
                                                  Message = string.Format("Транзакция {0} не реализована в шлюзе", transaction.Action),
                                                  Client = transaction.Client,
                                                  //TransactionId = transaction.TransactionId
                                              });
                        break;
                }
            }

            // если в процессе отправки транзакции произошла ошибка, отправляем сообщение об этом отправителю транзакции
            if (!error)
            {
                return;
            }

            logger.ErrorFormat("Ошибка при отправке заявки {0}. Ошибка: {1}", transaction.TransactionId, res);
            OnMessageReceived(new TransactionReply()
                {
                    Success = false,
                    Client = transaction.Client,
                    TransactionId = transaction.TransactionId,
                    ServiceCode = transaction.ServiceCode,
                    Message = res
                });
        }

        #region Overrides of GatewayService

        /// <summary>
        /// Запускает сервис.
        /// </summary>
        public override void Start()
        {
            connectionParamsForTransactions.USERID = loginForTransactions;
            apiWrapperForTransactions = MtesrlWrapper.GetInstance(connectionParamsForTransactions.ToString());

            if (apiWrapperForTransactions == null)
            {
                logger.FatalFormat("MicexOrderRouter не запущен. Не удалось подключиться к mtesrl.dll.");
                return;
            }


            logger.InfoFormat("MicexOrderRouter: Подключение к mtesrl.dll установлено.");

            apiWrapperForTransactions.Structure(out tableTypes, out transactionTypes);

            logger.DebugFormat("OrderStateProvider: Создаётся {0}OrderRouterAdapter.", micexSectionType);
            sectionOrderRouterAdapter = MicexSectionOrderRouterAdapter.CreateAdapter(micexSectionType, tableTypes, transactionTypes);
            logger.DebugFormat("OrderStateProvider: {0}OrderRouterAdapter создан.", micexSectionType);
            
         
            // получаем структуру таблиц текущего интерфейса шлюза            
            logger.DebugFormat("MicexOrderRouter: блокируем apiWrapperForTransactions.");
            lock (apiWrapperForTransactions)
            {
                logger.DebugFormat("MicexOrderRouter: apiWrapperForTransactions заблокирован.");
                #region Открываем таблицу счетов / получаем список счетов / закрываем таблицу

                MTETable clientsMteTable;

                logger.Debug("MicexOrderRouter: Открываем таблицу счетов");
                var clientsMteTableIdx = apiWrapperForTransactions.OpenTable(
                    tableTypes[sectionOrderRouterAdapter.AccountsTableIndex], string.Empty, true, out clientsMteTable);
                logger.Debug("MicexOrderRouter: Таблица счетов открыта");

                try
                {
                    logger.DebugFormat("MicexOrderRouter: Количество строк в таблице счетов: {0}", clientsMteTable.Rows.Length);

                    foreach (var row in clientsMteTable.Rows)
                    {
                        var clientAccount = sectionOrderRouterAdapter.GetAccountFromAccountsRow(row);
                        logger.DebugFormat("MicexOrderRouter: получен счёт {0}", clientAccount);
                        AvailableAccounts.Add(clientAccount);
                        Fills.Add(clientAccount, new Dictionary<Instrument, List<Fill>>());
                    }
                }
                catch(Exception ex)
                {
                    logger.ErrorFormat("Ошибка при считывании счетов из таблицы: {0}", ex.Message);
                }

                logger.Debug("MicexOrderRouter: Закрываем таблицу счетов");
                apiWrapperForTransactions.CloseTable(clientsMteTableIdx);
                logger.Debug("MicexOrderRouter: Таблица счетов закрыта");

                #endregion

            }

            orderStateProvider = OrderStateProvider.GetInstance(micexSectionType, connectionStringForDataUpdates, loginForDataUpdates, transactionsRefreshementTimeout);

            orderStateProvider.MessageReceived += OrderStateProviderMessageReceived;

            orderStateProvider.TryStart();

        }

        void OrderStateProviderMessageReceived(Message message)
        {
            var helper = message as OrderStateHelper;

            //если это OrderStateHelper, тогда происходит его обработка и принятие решения, своя это заявка или нет
            if (helper != null)
            {
                ProcessOrderStateHelper(helper);
                return;
            }

            var fill = message as Fill;

            if(fill != null)
            {
                ProcessFill(fill);
                return;
            }

            //обработка AccountMessage, во всех роутерах все эти сообщения обрабатываются
            var accountMessage = message as AccountMessage;

            if(accountMessage != null)
            {
                ProcessAccountMessage(accountMessage);
                return;
            }

        }


        void ProcessOrderStateHelper(OrderStateHelper helper)
        {
            uint transactId;

            // если ExtRef != null - значит сообщение пришло из OrderStateProvider (надо разбирать ExtRef)
            if (helper.ExtRef != null)
            {
                var extRef = helper.ExtRef;

                if (extRef.Length <= SessionIdLength)
                {
                    logger.DebugFormat("Получен статус по заявке, которая не отправлялась через раутер. extRef = {0}", extRef);
                    return;
                }

                var sessId = extRef.Substring(0, SessionIdLength);

                if (sessId != sessionId)
                    return;

                transactId = uint.Parse(extRef.Substring(SessionIdLength));
            }
            else if (helper.TransactionId != 0)
            {
                transactId = helper.TransactionId;
            }
            // новая фишка, шлюз может не присылать extRef в таблице после первого раза
            else if(helper.OrderExchangeId != 0)
            {
                lock (exchangeIdToTransactId)
                    if(!exchangeIdToTransactId.TryGetValue(helper.OrderExchangeId, out transactId))
                        return;
            }
            else
                return;

            OrderStateChangeMessage message;

            //в крит. секции собирается message для отправки
            lock (allOrders)
            {

                Order order;
                if (!allOrders.TryGetValue(transactId, out order))
                {
                    return;
                }


                //при дублировании событий из результата транзакции и из опроса таблиц, пропускаем их
                //если заявка уже всё
                if (order.State == OrderState.Filled || order.State == OrderState.Cancelled ||
                    // или стастус/количество не поменялись
                    ((order.State == helper.State || (order.State == OrderState.PartiallyFilled && helper.State == OrderState.Active)) && order.ActiveQty == helper.ActiveQty))
                {
                    return;
                }

                // если были недошедшие исполнения, то Cancelled отсылать рано ещё (для сообщения Cancelled из ответа транзакции)
                if (helper.State == OrderState.Cancelled && helper.ActiveQty != order.ActiveQty && helper.TransactionId != 0)
                {
                    return;
                }

                order.OrderExchangeId = helper.OrderExchangeId;

                var saldo = helper.ActiveQty;
                var executed = order.ActiveQty - saldo;
                order.ActiveQty = saldo;

                switch (helper.State)
                {
                    case OrderState.Active: //Активная
                        order.State = order.Quantity == order.ActiveQty ? OrderState.Active : OrderState.PartiallyFilled;
                        break;
                    case OrderState.Filled: //Исполнена
                        order.State = OrderState.Filled;
                        break;
                    default: //Снята
                        order.State = OrderState.Cancelled;
                        break;
                }

                message = new OrderStateChangeMessage
                              {
                                  Client = order.Client,
                                  TransactionId = order.TransactionId,
                                  OrderExchangeId = order.OrderExchangeId,
                                  State = order.State,
                                  ActiveQty = order.ActiveQty,
                                  FilledQty = executed
                              };

                logger.InfoFormat("OrderStateChanged {0} transId={1} state={2}", order.State, order.TransactionId, helper.State);
            }


            OnMessageReceived(message);
        }

        void ProcessFill(Fill fill)
        {
            OnMessageReceived(fill);
        }



        void ProcessAccountMessage(AccountMessage message)
        {
            OnMessageReceived(message);
            
            var position = message as Position;

            if (position != null)
            {
                lock (Portfolios)
                {
                    Dictionary<Instrument, Position> portfolio;

                    if (!Portfolios.TryGetValue(position.Account, out portfolio))
                    {
                        Portfolios.Add(position.Account, portfolio = new Dictionary<Instrument, Position>());
                        portfolio.Add(position.Instrument, position);
                    }
                    else
                    {
                        if (!portfolio.ContainsKey(position.Instrument))
                        {
                            portfolio.Add(position.Instrument, position);
                        }
                        else
                        {
                            portfolio[position.Instrument] = position;
                        }
                    }

                }

            }

            var moneyPosition = message as MoneyPosition;

            if (moneyPosition != null)
            {
                lock (Limits)
                {

                    if (!Limits.ContainsKey(moneyPosition.Account))
                    {
                        Limits.Add(moneyPosition.Account, moneyPosition);
                    }
                    else
                    {
                        Limits[moneyPosition.Account] = moneyPosition;
                    }
                }
            }
        }

        
        /// <summary>
        /// Останавливает сервис.
        /// </summary>
        public override void Stop()
        {
            orderStateProvider.Stop();
            try
            {
                logger.InfoFormat("Останавливаем MicexOrderRouter.");

                // закрываем все открытые таблицы
                if (apiWrapperForTransactions != null)
                {
                    lock (apiWrapperForTransactions)
                    {
                        apiWrapperForTransactions.Disconnect();
                    }
                }
            }
            catch (Exception exception)
            {
                logger.FatalFormat(
                    "Ошибка при остановке MicexOrderRouter:{0}: {1}, \n Stack:\n{2}",
                    sectionOrderRouterAdapter.GetType(),
                    exception.Message,
                    exception.StackTrace);
            }

            logger.InfoFormat("MicexOrderRouter остановлен.");
        }

        #endregion


        #region Parse
		
        static uint ParseOrderReply(string message, out long id)
        {
            //(163) Sell order #59905545 accepted (1 matched)
            //(163) Sell order #59905570 accepted (1 matched)
            //(161) Sell order #59905577 accepted
            //(163) Sell order #59905596 accepted (1 matched)

            uint filled;

            int sharpIndex = message.IndexOf('#') + 1;
            int spaceIndex = message.IndexOf(' ', sharpIndex);

            if (!long.TryParse(message.Substring(sharpIndex, spaceIndex - sharpIndex), out id))
                id = 0;

            int bracketIndex = message.IndexOf('(', sharpIndex) + 1;
            spaceIndex = message.IndexOf(' ', bracketIndex);

            if (!uint.TryParse(message.Substring(bracketIndex, spaceIndex - bracketIndex), out filled))
                filled = 0;


            return filled;
        }

        static uint ParseCancelReply(string message)
        {
            //(210) 1 order(s) with total balance 1 withdrawn, 0 order(s) not withdrawn
            //(210) 1 order(s) with total balance 4 withdrawn, 0 order(s) not withdrawn

            uint rest;

            const string balance = "balance";
            const string withdrawn = "withdrawn";

            int balanceIndex = message.IndexOf(balance) + balance.Length + 1;
            int withdrawnIndex = message.IndexOf(withdrawn, balanceIndex) - 1;

            if (!uint.TryParse(message.Substring(balanceIndex, withdrawnIndex - balanceIndex), out rest))
                rest = 0;


            return rest;
        }
        
        #endregion    
    }
}
