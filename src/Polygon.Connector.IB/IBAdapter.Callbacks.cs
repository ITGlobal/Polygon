//#define PARSE_INSTRUMENT_PARAMS

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using IBApi;
using ITGlobal.DeadlockDetection;
using OrderState = Polygon.Messages.OrderState;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed partial class IBAdapter
    {

#if PARSE_INSTRUMENT_PARAMS

        [Obsolete]
        private static InstrumentType? ParseInstrumentType(string securityType)
        {
            switch (securityType)
            {
                case "FUT":
                    return InstrumentType.Futures;
                case "FOP":
                    return InstrumentType.OptionOnFutures;
                case "OPT":
                    return InstrumentType.OptionOnEquity;
                case "STK":
                    return InstrumentType.Equity;
                case "IND":
                    return InstrumentType.Index;
                case "BOND":
                    return InstrumentType.Bond;
                default:
                    return null;
            }
        }

        [Obsolete]
        private const string ShortDateFormat = "yyyyMM";
        [Obsolete]
        private const string FullDateFormat = "yyyyMMdd";

        [Obsolete]
        private static DateTime? ParseExpiry(string expiry)
        {
            if (string.IsNullOrEmpty(expiry))
                return null;

            if (expiry.Length == 6)
            {
                return DateTime.ParseExact(expiry, ShortDateFormat, CultureInfo.InvariantCulture);
            }

            if (expiry.Length == 8)
            {
                return DateTime.ParseExact(expiry, FullDateFormat, CultureInfo.InvariantCulture);
            }

            return null;
        }

        [Obsolete]
        private static OptionType ParseOptionType(string rightType)
        {
            switch (rightType)
            {
                case "C":
                    return OptionType.Call;
                case "P":
                    return OptionType.Put;
                default:
                    return OptionType.Undefined;
            }
        }

#endif
        #region event handlers

        #region Обработка контрактов

        public override void contractDetails(int reqId, ContractDetails contractDetails)
        {
            contractDetailsContainer.AddContractDetails(reqId, contractDetails);
        }

        public override void contractDetailsEnd(int reqId)
        {
            ContractDetails contractDetails;
            if (!contractDetailsContainer.TryGetContractDetails(reqId, out contractDetails))
            {
                return;
            }
            contractDetailsContainer.RemoveTickerId(reqId);

            PendingTestResult testResult;
            if (pendingTestResultTickerContainer.TryGetPendingTestResult(reqId, out testResult))
            {
                testResult.Accept();
                pendingTestResultTickerContainer.RemoveTickerId(reqId);
            }

            Instrument instrument;
            if (contractDetailsTickers.TryGetInstrument(reqId, out instrument))
            {
                contractDetailsTickers.RemoveTickerId(reqId);
                var contract = contractDetails.Summary;
                connector.ContractContainer.PutContract(instrument, contract);

                var instrumentParams = connector.InstrumentParamsCache.GetInstrumentParams(instrument);

                #region Price step and precision
                uint decimals = 0;
                double step = contractDetails.MinTick * contractDetails.PriceMagnifier;

                while (Math.Round(step) != step)
                {
                    decimals++;
                    step *= 10;
                }

                instrumentParams.DecimalPlaces = decimals;
                instrumentParams.PriceStep = (decimal)contractDetails.MinTick * contractDetails.PriceMagnifier;

                #endregion

                var data = instrumentConverter.ResolveInstrumentAsync(this, instrument).Result;
                var instrumentType = data.InstrumentType;
                int multiplier = 1;
                if (contract.Multiplier != null && int.TryParse(contract.Multiplier, out multiplier))
                {
                    // Multiplier в качестве лота берём только для опционов на акции
                    if (instrumentType == IBInstrumentType.AssetOption)
                    {
                        instrumentParams.LotSize = multiplier;
                    }
                }

                switch (instrumentType)
                {
                    case IBInstrumentType.Commodity:
                    case IBInstrumentType.Equity:
                    case IBInstrumentType.Index:
                    case IBInstrumentType.FX:
                    case IBInstrumentType.AssetOption:
                        instrumentParams.PriceStepValue = instrumentParams.PriceStep;
                        break;
                    case IBInstrumentType.Future:
                    case IBInstrumentType.FutureOption:
                        instrumentParams.PriceStepValue = instrumentParams.PriceStep * multiplier;
                        break;
                }
           
                connector.IBFeed.Transmit(instrumentParams);
            }
        }

        #endregion

        #region Исторические данные

        public override void historicalData(
            int reqId,
            string date,
            double open,
            double high,
            double low,
            double close,
            int volume,
            int count,
            double WAP,
            bool hasGaps)
        {
            var dateValue = DateTime.ParseExact(date, new[] { "yyyyMMdd", "yyyyMMdd  HH\\:mm\\:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

            HistoricalDepthTickers.AddPoint(
                reqId,
                new HistoryDataPoint(dateValue, (decimal)high, (decimal)low, (decimal)open, (decimal)close, volume, count));
        }

        public override void historicalDataEnd(int reqId, string start, string end)
        {
            HistoricalDepthTickers.Complete(reqId);
        }

        #endregion

        #region Обработка статуса соединения

        public override void error(Exception e)
        {
            Log.Error().Print(e, e.Message);
            errorEvent.Set();
        }

        public override void error(string str)
        {
            Log.Error().Print(str);
            errorEvent.Set();
        }

        public override void error(int id, int errorCode, string errorMsg)
        {
            // See https://www.interactivebrokers.com/en/software/api/apiguide/tables/api_message_codes.htm

            switch (errorCode)
            {
                case 504:  // Not connected
                case 1100: // Connectivity between IB and TWS has been lost
                case 1300: // TWS socket port has been reset and this connection is being dropped. Please reconnect on the new port - <port_num>
                    Log.Error().Print(errorMsg);
                    errorEvent.Set();
                    connectedEvent.Reset();
                    disconnectedEvent.Set();
                    connector.RaiseConnectionStatusChanged(ConnectionStatus.Disconnected);
                    break;

                case 1102:
                    Log.Info().Print(errorMsg);
                    connectedEvent.Set();
                    disconnectedEvent.Reset();
                    connector.RaiseConnectionStatusChanged(ConnectionStatus.Connected);
                    break;

                case 162: // Historical market data Service error message.
                          // Historical data request pacing violation
                          // HMDS query returned no data
                    if (errorMsg.IndexOf("query returned no data", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // TODO ебаный стыд
                        HistoricalDepthTickers.NoMoreData(id);
                    }
                    else if (errorMsg.IndexOf("Historical data request pacing violation", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // TODO ебаный стыд
                        HistoricalDepthTickers.PaceViolation(id);
                    }
                    else if (errorMsg.IndexOf("Time length exceed max", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // TODO ебаный стыд
                        HistoricalDepthTickers.TimeLengthExceedMax(id);
                    }
                    else
                    {
                        HistoricalDepthTickers.Fail(id, errorMsg);
                    }
                    break;

                case 200:  // No security definition has been found for the request.
                    {
                        PendingTestResult testResult;
                        if (pendingTestResultTickerContainer.TryGetPendingTestResult(id, out testResult))
                        {
                            testResult.Reject();
                            pendingTestResultTickerContainer.RemoveTickerId(id);
                        }
                    }

                    HistoricalDepthTickers.Fail(id, errorMsg);

                    connector.ContractContainer.RejectContract(id);
                    Log.Error().Print(errorMsg);
                    break;

                case 202: // Order Canceled
                    break;

                case 501:  // Already connected
                case 2104: // A market data farm is connected. 
                case 2106: // A historical data farm is connected.
                    Log.Info().Print(errorMsg);
                    break;

                case 10092:
                    // Deep market data is not supported for this combination of security type/exchange
                    Instrument instrument;
                    if (marketDepthTickers.TryGetInstrument(id, out instrument))
                    {
                        Log.Error().Print($"Unable to retreive order book on {instrument.Code}. {errorMsg.Preformatted()}");
                        break;
                    }
                    goto default;

                case 110: // The price does not conform to the minimum price variation for this contract.
                case 201: // Order rejected - reason:THIS ACCOUNT MAY NOT HOLD SHORT STOCK POSITIONS.
                case 203: // The security <security> is not available or allowed for this account

                    HistoricalDepthTickers.Fail(id, errorMsg);

                    using (orderInfoContainerLock.Lock())
                    {
                        OrderInfo orderInfo;
                        if (!orderInfoContainer.TryGetByTickerId(id, out orderInfo))
                        {
                            // Неизвестная заявка, ничего не поделаешь
                            goto default;
                        }

                        orderInfo.State = OrderState.Error;

                        // Вычисляем OSCM
                        var oscm = new OrderStateChangeMessage
                        {
                            TransactionId = orderInfo.NewOrderTransactionId,
                            ActiveQuantity = (uint)orderInfo.ActiveQuantity,
                            ChangeTime = DateTime.Now,
                            FilledQuantity = (uint)(orderInfo.Quantity - orderInfo.ActiveQuantity),
                            OrderExchangeId = orderInfo.PermId.ToString(CultureInfo.InvariantCulture),
                            Quantity = (uint?)(orderInfo.Quantity),
                            State = OrderState.Error
                        };

                        // Выплевываем OSCM
                        connector.IBOrderRouter.Transmit(oscm);

                        Log.Error().Print($"Unable to place order {orderInfo.PermId}. {errorMsg.Preformatted()}");
                    }
                    break;

                default:
                    // Warning codes range
                    if (errorCode >= 2100 && errorCode <= 2110)
                    {
                        Log.Warn().Print(errorMsg);
                    }
                    else
                    {
                        PendingTestResult testResult;
                        Instrument subscriptionInstrument;
                        if (pendingTestResultTickerContainer.TryGetPendingTestResult(id, out testResult))
                        {
                            testResult.Reject();
                            pendingTestResultTickerContainer.RemoveTickerId(id);
                        }
                        else if (marketDataTickers.TryGetInstrumentAndPendingTestResult(id, out subscriptionInstrument, out testResult))
                        {
                            testResult?.Reject();
                            marketDataTickers.RemoveTickerId(id);
                        }

                        Log.Error().Print($"IB error #{errorCode}: {errorMsg.Preformatted()}");
                        errorEvent.Set();

                        HistoricalDepthTickers.Fail(id, errorMsg);
                    }
                    break;
            }

            // TODO если пришла ошибка по тикеру - тикер нужно убить?
        }

        public override void nextValidId(int orderId)
        {
            Log.Debug().PrintFormat("nextValidId: {0}", orderId);

            // nextValidId() приходит после установки соединения
            // SDK советует использовать это событие как маркер установки соединения
            Interlocked.Exchange(ref nextValidIdentifyer, orderId);

            connectedEvent.Set();
            disconnectedEvent.Reset();
        }

        public override void connectionClosed()
        {
            Log.Debug().Print("connectionClosed");

            connectedEvent.Reset();
            disconnectedEvent.Set();
        }

        #endregion

        #region Обработка тиков

        public override void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            if (Log.IsTraceEnabled)
            {
                Log.Trace().PrintFormat("< tickPrice {0} {1} {2} {3}", tickerId, field, price, canAutoExecute);
            }

            HandleTick(
                tickerId,
                field,
                price,
                x => !double.IsNaN(x),
                (instrumentParams, type, value) =>
                {
                    switch (field)
                    {
                        case TickType.BID:
                            instrumentParams.BestBidPrice = (decimal)value;
                            return true;

                        case TickType.ASK:
                            instrumentParams.BestOfferPrice = (decimal)value;
                            return true;

                        case TickType.LAST:
                            instrumentParams.LastPrice = (decimal)value;
                            return true;

                        case TickType.CLOSE:
                            // Если LastPrice еще не пришел, проставляем ему последнее известное значение
                            if (instrumentParams.LastPrice == 0M)
                            {
                                instrumentParams.LastPrice = (decimal)value;
                                return true;
                            }
                            return false;

                        case TickType.HIGH:
                        case TickType.LOW:
                        case TickType.OPEN:
                        case TickType.LOW_13_WEEK:
                        case TickType.HIGH_13_WEEK:
                        case TickType.LOW_26_WEEK:
                        case TickType.HIGH_26_WEEK:
                        case TickType.LOW_52_WEEK:
                        case TickType.HIGH_52_WEEK:
                            return false;

                        default:
                            return false;
                    }
                });
        }

        public override void tickSize(int tickerId, int field, int size)
        {
            if (Log.IsTraceEnabled)
            {
                Log.Trace().PrintFormat("< tickSize {0} {1} {2}", tickerId, field, size);
            }
            
            HandleTick(
                tickerId,
                field,
                size,
                handler: (instrumentParams, type, value) =>
                {
                    switch (field)
                    {
                        case TickType.BID_SIZE:
                            instrumentParams.BestBidQuantity = value;
                            return true;

                        case TickType.ASK_SIZE:
                            instrumentParams.BestOfferQuantity = value;
                            return true;

                        case TickType.LAST_SIZE:
                        case TickType.VOLUME:
                        default:
                            return false;
                    }
                });
        }

        private void HandleTick<T>(int tickerId, int field, T value, Func<T, bool> valueFilter = null, Func<InstrumentParams, int, T, bool> handler = null)
        {
            // Параметр handler сделан опциональным для того, чтобы опциональный параметр valueFilter поместить перед ним
            // Это сделано исключительно ради читабельности

            if (handler == null)
            {
                return;
            }

            if (valueFilter != null && !valueFilter(value))
            {
                return;
            }

            Instrument instrument;
            PendingTestResult pendingTestResult;
            if (marketDataTickers.TryGetInstrumentAndPendingTestResult(tickerId, out instrument, out pendingTestResult))
            {
                pendingTestResult?.Accept();

                var instrumentParams = connector.InstrumentParamsCache.GetInstrumentParams(instrument);
                if (handler(instrumentParams, field, value))
                {
                    connector.IBFeed.Transmit(instrumentParams);
                }
            }
        }

        #endregion

        #region Стаканы

        public override void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            // Определяем инструмент по тикеру
            Instrument instrument;
            if (!marketDepthTickers.TryGetInstrument(tickerId, out instrument))
            {
                // Неизвестный тикер
                return;
            }

            // Получаем построитель стакана по инструменту
            var builder = mktDepthBuilders.Get(instrument);

            // Обновляем стакан и передаем его через фид
            var orderBook = builder.Update(position, operation, side, position, size);
            connector.IBFeed.Transmit(orderBook);
        }

        public override void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
        {
            updateMktDepth(tickerId, position, operation, side, price, size);
        }

        #endregion

        #region Счета и позиции

        public override void updateAccountValue(string key, string value, string currency, string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return;
            }

            var moneyPosition = moneyPositions.Get(accountName, account => new MoneyPosition { Account = account });

            decimal decimalValue;
            if (!decimal.TryParse(value, out decimalValue))
            {
                return;
            }

            var updateProfitLoss = false;
            switch (key)
            {
                case AccountSummaryTags.FullInitMarginReq:
                    moneyPosition[MoneyPositionPropertyNames.FullInitMarginReq] = decimalValue;
                    break;
                case AccountSummaryTags.FullMaintMarginReq:
                    moneyPosition[MoneyPositionPropertyNames.FullMaintMarginReq] = decimalValue;
                    break;
                case AccountSummaryTags.RealizedPnL:
                    moneyPosition[MoneyPositionPropertyNames.RealizedProfitLoss] = decimalValue;
                    updateProfitLoss = true;
                    break;
                case AccountSummaryTags.UnrealizedPnL:
                    moneyPosition[MoneyPositionPropertyNames.UnrealizedProfitLoss] = decimalValue;
                    updateProfitLoss = true;
                    break;
                case AccountSummaryTags.TotalCashBalance:
                    moneyPosition[MoneyPositionPropertyNames.TotalCashBalance] = decimalValue;
                    break;

                default:
                    return;
            }

            if (updateProfitLoss)
            {
                var realizedPnL = moneyPosition[MoneyPositionPropertyNames.RealizedProfitLoss].AsDecimal();
                var unrealizedPnL = moneyPosition[MoneyPositionPropertyNames.UnrealizedProfitLoss].AsDecimal();

                moneyPosition[MoneyPositionPropertyNames.ProfitLoss] = realizedPnL != null && unrealizedPnL != null
                    ? realizedPnL + unrealizedPnL
                    : null;
            }

            // Выплевываем наружу сообщение
            connector.IBOrderRouter.Transmit(moneyPosition);
        }

        public override void accountDownloadEnd(string account)
        {
            // Подписываемся на обновления по счету
            Log.Debug().PrintFormat("< accountDownloadEnd {0}", account);
            Socket.reqAccountUpdates(true, account);

            // Запрашиваем сделки
            var ticketId = NextTickerId();
            Socket.reqExecutions(ticketId, new ExecutionFilter { AcctCode = account });
        }

        public override void position(string account, Contract contract, int pos, double avgCost)
        {
            Log.Debug().PrintFormat("< position {0} {1} {2} {3}", account, contract.ConId, pos, avgCost);
            connector.ContractContainer.GetInstrumentAsync(
                contract,
                $"A position in account \"{account}\"",
                instrument =>
                {
                    var position = new PositionMessage
                    {
                        Account = account,
                        Instrument = instrument,
                        Quantity = pos,
                        Price = (decimal)avgCost
                    };

                    int multiplier;
                    if (contract.Multiplier != null && int.TryParse(contract.Multiplier, out multiplier) && multiplier > 1)
                    {
                        position.Price /= multiplier;
                    }

                    // Выплевываем наружу сообщение
                    connector.IBOrderRouter.Transmit(position);
                });
        }

        #endregion

        #region Сделки

        private readonly ILockObject lastExecCumQtiesLock = DeadlockMonitor.Cookie<IBAdapter>("lastExecCumQtiesLock");
        private readonly Dictionary<Tuple<Instrument, int>, int> lastExecCumQties = new Dictionary<Tuple<Instrument, int>, int>();


        public override void execDetails(int reqId, Contract contract, Execution execution)
        {
            //Сделки с нулевым количеством
            //Пропускаем BAG, т.к. это контракт для конструкций. Сейчас мы его не используем.
            //BAG is the security type for COMBO order
            //https://www.interactivebrokers.com/en/software/api/apiguide/c/placing_a_combination_order.htm
            //https://www.interactivebrokers.com/en/software/api/apiguide/tables/api_message_codes.htm
            if (contract.SecType == "BAG")
            {
                return;
            }

            Log.Debug().PrintFormat("< execDetails {0} {1} {2} {3}", reqId, contract.ConId, execution.ExecId);

            connector.ContractContainer.GetInstrumentAsync(
                contract,
                string.Format("A fill on order \"{0}\", account \"{1}\"", execution.ExecId, execution.AcctNumber),
                async instrument =>
                {
                    var fill = new FillMessage
                    {
                        Account = execution.AcctNumber,
                        Instrument = instrument,
                        DateTime = ParseExecutionTime(execution.Time),
                        ExchangeId = execution.ExecId,
                        ExchangeOrderId = execution.PermId.ToString(CultureInfo.InvariantCulture),
                        Operation = execution.Side == "BOT" ? OrderOperation.Buy : OrderOperation.Sell,
                        Price = (decimal)execution.Price,
                        Quantity = (uint)execution.CumQty
                    };

                    // В случае частичного исполнения заявки мы запоминаем прежнее значение CumQty
                    // и в соответствии с ним пересчитываем fill.Quantity
                    using (lastExecCumQtiesLock.Lock())
                    {
                        var orderKey = new Tuple<Instrument, int>(instrument, execution.PermId);
                        int lastQty;
                        if (lastExecCumQties.TryGetValue(orderKey, out lastQty))
                        {
                            fill.Quantity = (uint)(execution.CumQty - lastQty);
                        }

                        lastExecCumQties[orderKey] = execution.CumQty;
                    }

                    // Ждём пока в наш адаптер не придут данные по заявке
                    await orderInfoContainer.WaitOrderForFill(execution);

                    // Выплевываем наружу сообщение
                    connector.IBOrderRouter.Transmit(fill);
                });
        }

        #endregion

        #region Заявки

        public override void orderStatus(
            int orderId,
            string status,
            int filled,
            int remaining,
            double avgFillPrice,
            int permId,
            int parentId,
            double lastFillPrice,
            int clientId,
            string whyHeld)
        {
            using (orderInfoContainerLock.Lock())
            {
                OrderInfo orderInfo;
                if (!orderInfoContainer.TryGetByTickerId(orderId, out orderInfo) &&
                    !orderInfoContainer.TryGetByPermId(permId, out orderInfo))
                {
                    // Неизвестная заявка, ничего не поделаешь
                    return;
                }

                var orderState = IBUtils.ParseOrderState(status) ?? orderInfo.State;

                // Если заявка снялась по запросу из OW, надо выплюнуть TransactionReply
                if (orderState == OrderState.Cancelled &&
                    orderInfo.KillOrderTransactionId != null &&
                    !orderInfo.KillOrderTransactionReplySent)
                {
                    connector.IBOrderRouter.Transmit(new TransactionReply
                    {
                        TransactionId = orderInfo.KillOrderTransactionId.Value,
                        Success = true
                    });
                    orderInfo.KillOrderTransactionReplySent = true;
                }

                // Вычисляем OSCM
                var message = new OrderStateChangeMessage
                {
                    ActiveQuantity = (uint)remaining,
                    ChangeTime = DateTime.Now,
                    FilledQuantity = (uint)filled,
                    OrderExchangeId = permId.ToString(CultureInfo.InvariantCulture),
                    Quantity = (uint?)(filled + remaining),
                    TransactionId = orderInfo.NewOrderTransactionId,
                    State = orderState
                };

                if (!double.IsNaN(lastFillPrice) &&
                    Math.Abs(lastFillPrice) > double.Epsilon)
                {
                    message.Price = (decimal?)lastFillPrice;
                }

                if (message.State != null)
                {
                    orderInfo.State = message.State.Value;
                }
                if (message.ActiveQuantity != null)
                {
                    orderInfo.ActiveQuantity = (int)message.ActiveQuantity.Value;
                }

                // Выплевываем OSCM
                connector.IBOrderRouter.Transmit(message);

                // Пробуем отправить ожидающие филы
                orderInfoContainer.ProcessPendingFills(orderInfo);
            }
        }

        public override void openOrder(int orderId, Contract contract, IBApi.Order order, IBApi.OrderState orderState)
        {
            using (orderInfoContainerLock.Lock())
            {
                OrderInfo orderInfo;
                if (orderInfoContainer.TryGetByTickerId(orderId, out orderInfo) ||
                    orderInfoContainer.TryGetByPermId(order.PermId, out orderInfo))
                {
                    // Заявка уже есть в раутере

                    // Запомним ее PermId
                    orderInfo.PermId = order.PermId;
                    if (order.PermId != 0)
                    {
                        orderInfoContainer.StoreByPermId(order.PermId, orderInfo);
                    }

                    // Если по ней есть транзакция, и она еще не подтверждена, то нужно отправить TransactionReply
                    if (!orderInfo.IsExternal &&
                        !orderInfo.NewOrderTransactionReplySent)
                    {
                        connector.IBOrderRouter.Transmit(new TransactionReply
                        {
                            Success = true,
                            TransactionId = orderInfo.NewOrderTransactionId
                        });
                        orderInfo.NewOrderTransactionReplySent = true;
                    }
                    return;
                }

                // Новая заявка, ее еще нет в раутере. Нужно решить, что с нею делать
                switch (connector.IBOrderRouter.ModeInternal)
                {
                    case OrderRouterMode.ThisSessionOnly:
                        // Невозобновляемая сессия, раутер не должен подцеплять старые заявки, в игнор ее
                        return;

                    case OrderRouterMode.ThisSessionRenewable:
                        // Возобновляемая сессия, раутер должен подцеплять старые заявки с фильтрацией по комментарию
                        if (!connector.IBOrderRouter.FilterByComment(order.OrderRef))
                        {
                            // Чужая заявка, в игнор ее
                            return;
                        }
                        break;

                    case OrderRouterMode.ExternalSessionsRenewable:
                        // Возобновляемая сессия, раутер должен подцеплять старые заявки без фильтрации по комментарию
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("OrderRouterMode");
                }

                // Запоминаем эту заявку
                // Инструмент у нее будет проставлен ниже
                orderInfo = new OrderInfo(order, orderState: orderState.Status)
                {
                    IsExternal = true,
                    ShouldEmitExternalOrderMessage = true
                };

                if (orderId != 0)
                {
                    orderInfoContainer.StoreByTickerId(orderId, orderInfo);
                }

                if (order.PermId != 0)
                {
                    orderInfoContainer.StoreByPermId(order.PermId, orderInfo);
                }

                // Резолвим ее инструмент
                connector.ContractContainer.GetInstrumentAsync(
                    contract,
                    $"A order \"{orderInfo.OrderId}\", account \"{orderInfo.Account}\"",
                    instrument =>
                    {
                        using (orderInfoContainerLock.Lock())
                        {
                            // После резолва выплевываем заявку
                            orderInfo.Instrument = instrument;

                            if (orderInfo.ShouldEmitExternalOrderMessage)
                            {
                                // Выплевываем внешнюю заявку
                                connector.IBOrderRouter.Transmit(new ExternalOrderMessage { Order = orderInfo.CreateOrder() });
                                orderInfo.ShouldEmitExternalOrderMessage = false;
                            }
                        }
                    });
            }
        }

        #endregion

        #endregion
    }
}

