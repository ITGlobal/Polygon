using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Connector.MicexBridge.Feed;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
	public class OrderStateProvider 
	{
        #region GetInstance

	    private static Dictionary<string, OrderStateProvider> instances = new Dictionary<string, OrderStateProvider>();

        /// <summary>
        /// Получаем существующий провайдер для данных по уникальному логину, если его нет, создаём
        /// </summary>
        public static OrderStateProvider GetInstance(MicexSecionType sectionType,
            string connectionStringForDataUpdates,
            string loginForDataUpdates,
            int transactionsRefreshementTimeout)
        {
            OrderStateProvider rProvider;
            lock (instances)
            {
                if(!instances.TryGetValue(loginForDataUpdates, out rProvider))
                {
                    instances.Add(loginForDataUpdates, rProvider = new OrderStateProvider(sectionType, connectionStringForDataUpdates, loginForDataUpdates, transactionsRefreshementTimeout));

                    //rProvider.Start();
                }
            }

            return rProvider;
        }

        #endregion

		#region Private members

		/// <summary>
		/// Интерфейс логгера.
		/// </summary>
		private readonly ILog logger;

        /// <summary>
        /// Секция ММВБ.
        /// </summary>
        private readonly MicexSecionType micexSectionType;

		/// <summary>
		/// Адаптер к данным раутинга для конкретной секции ММВБ.
		/// </summary>
		private MicexSectionOrderRouterAdapter sectionOrderRouterAdapter;

        /// <summary>
        /// Адаптер к анонимным данным конкретной секции ММВБ. В раутере он нужен для парсинга таблицы параметров.
        /// </summary>
        private MicexSectionFeedAdapter sectionFeedAdapter;


        /// <summary>
        /// Контэйнер парамтеров инструментов. Заполняется один раз при старте сервиса.
        /// </summary>
        private Dictionary<string, InstrumentParams> instrumentsParams;


		/// <summary>
		/// Задача, в которой происходит обновление данных, периодический опрос шлюза.
		/// </summary>
		private readonly Task dataRefreshementTask;

		/// <summary>
		/// Задача, в которой проверяются обновления позиций и лимитов по деньгам.
		/// </summary>
		private readonly Task positionsRefreshementTask;

		/// <summary>
		/// Обертка над mtesrl.dll которую мы используем для получения из шлюза обновлений данных о состоянии заявок, 
		/// своих сделках, позациях, лимитах по деньгами и т.п.
		/// </summary>
		private MtesrlWrapper apiWrapperForDataUpdates;

		/// <summary>
		/// Переметры подключения к шлюзу.
		/// </summary>
		private readonly ConnectionParameters connectionParamsForDataUpdates;

		/// <summary>
		/// Логин с которым происходит подключение к шлюзу для обновления данных.
		/// </summary>
		private readonly string loginForDataUpdates;

		/// <summary>
		/// Набор таблиц доступных в данном интерфейсе шлюза.
		/// </summary>
		private TableType[] tableTypes;

        /// <summary>
        /// Типы транзакций в данном интерфейсе шлюза.
        /// </summary>
        private TransactionType[] transactionTypes;

		/// <summary>
		/// Индекс таблицы параметров инструментов.
		/// </summary>
		private int orderTableIdx;

		/// <summary>
		/// Индекс таблицы всех сделок.
		/// </summary>
		private int dealTableIdx;

		/// <summary>
		/// Индекс таблицы ограничений по деньгам.
		/// </summary>
		private int moneyTableIdx;

		/// <summary>
		/// Индекс табицы позиций по счетам.
		/// </summary>
		private int positionTableIdx;

		/// <summary>
		/// Период опроса таблицы заявок (в миллисекундах).
		/// </summary>
		private readonly int transactionsRefreshementTimeout;
		
		private readonly CancellationTokenSource cancelSource = new CancellationTokenSource();

		private readonly CancellationToken token;

		#endregion

		private OrderStateProvider(MicexSecionType sectionType,
			string connectionStringForDataUpdates,
			string loginForDataUpdates,
			int transactionsRefreshementTimeout)
		{
			logger = LogManager.GetLogger(GetType());
			micexSectionType = sectionType;
			// Code = name;

			this.transactionsRefreshementTimeout = transactionsRefreshementTimeout;


			this.loginForDataUpdates = loginForDataUpdates;

			connectionParamsForDataUpdates = new ConnectionParameters(connectionStringForDataUpdates);

			token = cancelSource.Token;
			dataRefreshementTask = new Task(OrdersStateAndTradesRefreshementProc, token);
			positionsRefreshementTask = new Task(LimitsAndPositionsRefreshementProc, token);
            //logger.DebugFormat("Создан MicexOrderRouter для секции {0}. Идентификатор сессии {1}", sectionType, sessionId);
		}

		#region Overrides of GatewayService

	    private bool isSatrted = false;
        public void TryStart()
        {
            if (isSatrted) 
                return;

            Start();
            isSatrted = true;
        }

		/// <summary>
		/// Запускает сервис.
		/// </summary>
        void Start()
        {
            connectionParamsForDataUpdates.USERID = loginForDataUpdates;
            apiWrapperForDataUpdates = MtesrlWrapper.GetInstance(connectionParamsForDataUpdates.ToString());

            if (apiWrapperForDataUpdates == null)
            {
                logger.FatalFormat("MicexOrderRouter не запущен. Не удалось подключиться к mtesrl.dll.");
                return;
            }

            logger.InfoFormat("OrderStateProvider: Подключение к mtesrl.dll в установлено.");

            apiWrapperForDataUpdates.Structure(out tableTypes, out transactionTypes);

            // создаём адаптер, соответствующий секции ММВБ);

            logger.DebugFormat("OrderStateProvider: Создаётся {0}OrderRouterAdapter.", micexSectionType);
            sectionOrderRouterAdapter = MicexSectionOrderRouterAdapter.CreateAdapter(micexSectionType, tableTypes, transactionTypes);
            logger.DebugFormat("OrderStateProvider: {0}OrderRouterAdapter создан.", micexSectionType);

            logger.DebugFormat("OrderStateProvider: Создаётся {0}FeedAdapter.", micexSectionType);
            sectionFeedAdapter = MicexSectionFeedAdapter.CreateAdapter(micexSectionType, tableTypes);
            logger.DebugFormat("OrderStateProvider: {0}FeedAdapter создан.", micexSectionType);


            logger.DebugFormat("OrderStateProvider: блокируем apiWrapperForDataUpdates.");
            // получаем структуру таблиц текущего интерфейса шлюза
            lock (apiWrapperForDataUpdates)
            {
                logger.DebugFormat("OrderStateProvider: apiWrapperForDataUpdates заблокирован.");

                #region Открываем таблицу параметров и сохраняем все инструменты в контэйнере InstrumentsParams

                instrumentsParams = new Dictionary<string, InstrumentParams>();

                MTETable infoMteTable;
                var infoTableIdx = apiWrapperForDataUpdates.OpenTable(
                    tableTypes[sectionOrderRouterAdapter.InfoTableIndex],
                    sectionFeedAdapter.InfoTableParams,
                    true,
                    out infoMteTable);

                foreach (var row in infoMteTable.Rows)
                {
                    var code = sectionFeedAdapter.GetInstrumentCodeFromParams(row);
                    var instrument = new Instrument
                    {
                        Code = code,
                        ClassCode = sectionFeedAdapter.ClassCode(row)
                    };

                    if (!instrumentsParams.ContainsKey(code))
                    {
                        instrumentsParams.Add(
                            code,
                            new InstrumentParams
                            {
                                Instrument = instrument,
                                DecimalPlaces = (uint)sectionFeedAdapter.GetDecimals(row)
                            });
                    }

                    logger.InfoFormat("Добавляем инструмент {0}", instrument);
                }

                apiWrapperForDataUpdates.CloseTable(infoTableIdx);

                #endregion


                MTETable dealMteTable;
                dealTableIdx = apiWrapperForDataUpdates.OpenTable(
                    tableTypes[sectionOrderRouterAdapter.DealTableIndex], string.Empty, false, out dealMteTable);
                UpdateFills(dealMteTable);

                MTETable orderMteTable;
                orderTableIdx = apiWrapperForDataUpdates.OpenTable(
                    tableTypes[sectionOrderRouterAdapter.OrderTableIndex], string.Empty, false, out orderMteTable);
                UpdateOrders(orderMteTable);

                if (micexSectionType != MicexSecionType.Currency)
                {
                    MTETable moneyMteTable;
                    moneyTableIdx = apiWrapperForDataUpdates.OpenTable(
                        tableTypes[sectionOrderRouterAdapter.MoneyTableIndex],
                        sectionOrderRouterAdapter.MoneyTableOpenParams,
                        false,
                        out moneyMteTable);
                    UpdateMoney(moneyMteTable);
                }


                MTETable positionsMteTable;
                positionTableIdx = apiWrapperForDataUpdates.OpenTable(
                    tableTypes[sectionOrderRouterAdapter.PositionsTableIndex],
                    sectionOrderRouterAdapter.GetPositionsTableParams(tableTypes[sectionOrderRouterAdapter.PositionsTableIndex].Input),
                    false,
                    out positionsMteTable);
                UpdatePositions(positionsMteTable);



            }

            dataRefreshementTask.Start();
            positionsRefreshementTask.Start();
        }

		/// <summary>
		/// Останавливает сервис.
		/// </summary>
        public void Stop()
        {
            try
            {
                if (cancelSource.IsCancellationRequested)
                {
                    return;
                }

                logger.InfoFormat("Останавливаем MicexOrderRouter.");

                cancelSource.Cancel();
                try
                {
                    dataRefreshementTask.Wait();
                }
                catch (AggregateException)
                {
                }

                try
                {
                    positionsRefreshementTask.Wait();
                }
                catch (AggregateException)
                {
                }

                // закрываем все открытые таблицы
                if (apiWrapperForDataUpdates != null)
                {
                    lock (apiWrapperForDataUpdates)
                    {
                        apiWrapperForDataUpdates.CloseTable(orderTableIdx);
                        apiWrapperForDataUpdates.CloseTable(dealTableIdx);

                        // отключаемся от mtesrl.dll.
                        apiWrapperForDataUpdates.Disconnect();
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


        public decimal GetDecimalsForInstrument(Instrument instrument)
        {
            return instrumentsParams[instrument.Code].DecimalPlaces;
        }

	    #endregion

    
		#region Private methods


		/// <summary>
		/// В этом методе в цикле происходит обновление данных о статусах заявок и о сделках.
		/// </summary>
		private void OrdersStateAndTradesRefreshementProc()
		{
			logger.InfoFormat("Запуск задача dataRefreshementTask.");

			while (!token.IsCancellationRequested)
			{
				try
				{
					MTETable[] mteTables;
					lock (apiWrapperForDataUpdates)
					{
						apiWrapperForDataUpdates.AddTable(orderTableIdx, sectionOrderRouterAdapter.OrderTableIndex);
						apiWrapperForDataUpdates.AddTable(dealTableIdx, sectionOrderRouterAdapter.DealTableIndex);

						mteTables = apiWrapperForDataUpdates.Refresh(tableTypes);
					}

					foreach (var table in mteTables)
					{
						if (table.Ref == sectionOrderRouterAdapter.OrderTableIndex)
						{
							UpdateOrders(table);
						}
						else if (table.Ref == sectionOrderRouterAdapter.DealTableIndex)
						{
                            UpdateFills(table);
						}
					}
				}
				catch (Exception ex)
				{
					logger.ErrorFormat(
						"Ошибка при обновлении состояний заявок/сделок. \n Ошибка: {0} \n, Stack: {1}", ex.Message, ex.StackTrace);
				}

				Thread.Sleep(transactionsRefreshementTimeout);
			}

			logger.InfoFormat("Задача dataRefreshementTask завершена.");
		}

		/// <summary>
		/// Этот метод вешается на задачу обновления лимитов и позиций.
		/// </summary>
		private void LimitsAndPositionsRefreshementProc()
		{
			logger.InfoFormat("Запуск задачи positionsRefreshementTask.");


			while (!token.IsCancellationRequested)
			{
				try
				{
					MTETable[] mteTables;
					lock (apiWrapperForDataUpdates)
					{
						if (sectionOrderRouterAdapter.MoneyTableIndex > 0)
						{
							apiWrapperForDataUpdates.AddTable(moneyTableIdx, sectionOrderRouterAdapter.MoneyTableIndex);
						}

						apiWrapperForDataUpdates.AddTable(positionTableIdx, sectionOrderRouterAdapter.PositionsTableIndex);

						mteTables = apiWrapperForDataUpdates.Refresh(tableTypes);
					}

					foreach (var table in mteTables)
					{
						if (table.Ref == sectionOrderRouterAdapter.MoneyTableIndex)
						{
							foreach (var moneyPosition in UpdateMoney(table))
							{
								OnMessageReceived(moneyPosition);
							}
						}
						else if (table.Ref == sectionOrderRouterAdapter.PositionsTableIndex)
						{
							foreach (var newPosition in UpdatePositions(table))
							{
								OnMessageReceived(newPosition);
							}
						}
					}
				}
				catch (Exception ex)
				{
					logger.ErrorFormat(
						"Ошибка при обновлении таблиц денег и позиций.\nОшбика: {0} \nStack: {1}", ex.Message, ex.StackTrace);
				}

				Thread.Sleep(100);
			}

			logger.InfoFormat("Задача positionsRefreshementTask завершена.");
		}

        private void UpdateOrders(MTETable orderMteTable)
        {
            foreach (var orderRow in orderMteTable.Rows)
            {
               long orderExchangeId = sectionOrderRouterAdapter.GetOrderIdFromOrderRow(orderRow);

                var saldo = (uint)sectionOrderRouterAdapter.GetRestFromOrderRow(orderRow);

                OrderState oState;

                var state = sectionOrderRouterAdapter.GetOrderStateFromOrderRow(orderRow);
                switch (state)
                {
                    case "O": //Активная
                        oState = OrderState.Active;
                        break;
                    case "M": //Исполнена
                        oState = OrderState.Filled;
                        break;
                    default: //Снята
                        oState = OrderState.Cancelled;
                        break;
                }



                var extRef = sectionOrderRouterAdapter.GetExtRefFromOrderRow(orderRow);


                OnMessageReceived(
                    new OrderStateHelper
                        {
                            OrderExchangeId = orderExchangeId,
                            State = oState,
                            ActiveQty = saldo,
                            ExtRef = extRef,
                        });
            }
        }

		private void UpdateFills(MTETable dealsMteTable)
		{
            foreach (MTERow fillRow in dealsMteTable.Rows)
            {
                Instrument instrument = sectionOrderRouterAdapter.GetInstrumentFromFillRow(fillRow);



                long id = sectionOrderRouterAdapter.GetIdFromFillRow(fillRow);
                long idOrder = sectionOrderRouterAdapter.GetOrderIdFromFillRow(fillRow);
                

                Fill fill = sectionOrderRouterAdapter.GetFillFromRow(fillRow, (int)GetDecimalsForInstrument(instrument));
                fill.Instrument = instrument;
                fill.ExchangeId = id;
                fill.ExchangeOrderId = idOrder;

                OnMessageReceived(fill);
            }


			//foreach (MTERow dealRow in dealsMteTable.Rows)
			//{
			//    int orderId = sectionOrderRouterAdapter.GetOrderIdFromDealRow(dealRow);
			//    Order posOrder = null;

			//    foreach (Order order in orders.Values)
			//        if (order.order_key == orderId)
			//        {
			//            posOrder = order;
			//            break;
			//        }

			//    if (posOrder == null)
			//        continue;

			//    int id = sectionOrderRouterAdapter.GetIdFromDealRow(dealRow);

			//    int decimals = quoteRouter.GetInstrumentInfo(posOrder.instrument).Decimals;

			//    Position position = sectionOrderRouterAdapter.GetPositionFromDealRow(dealRow, posOrder, decimals);

			//}
		}

		private IEnumerable<AccountMessage> UpdateMoney(MTETable moneyMteTable)
		{
			var rValue = new Dictionary<string, AccountMessage>();

			foreach (var moneyPosition in moneyMteTable.Rows.Select(row => sectionOrderRouterAdapter.UpdateMoneyPosition(row)))
			{
				if (!rValue.ContainsKey(moneyPosition.Account))
				{
					rValue.Add(moneyPosition.Account, moneyPosition);
				}
				else
				{
					rValue[moneyPosition.Account] = moneyPosition;
				}
			}

			return rValue.Values;
		}

        private IEnumerable<AccountMessage> UpdatePositions(MTETable positionsMteTable)
        {
            var rValue = new Dictionary<string, AccountMessage>();

            foreach (var row in positionsMteTable.Rows)
            {
                var position = sectionOrderRouterAdapter.UpdatePos(row);

                if(string.IsNullOrEmpty(position.Instrument.ClassCode))
                {
                    InstrumentParams par;
                    if (instrumentsParams.TryGetValue(position.Instrument.Code, out par))
                        position.Instrument.ClassCode = par.Instrument.ClassCode;
                }

                if (!rValue.ContainsKey(position.Account))
                {
                    rValue.Add(position.Account, position);
                }
                else
                {
                    rValue[position.Account] = position;
                }
            }

            return rValue.Values;
        }

		#endregion

        protected void OnMessageReceived(Message message)
        {
            if (MessageReceived != null)
            {
                foreach (Action<Message> action in MessageReceived.GetInvocationList())
                {
                    Action<Message> a = action;
                    ThreadPool.QueueUserWorkItem(s => a(message));
                }
            }
        }

        public event Action<Message> MessageReceived;
	}

}