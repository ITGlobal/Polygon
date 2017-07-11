﻿using System;
using System.Collections.Generic;
using Polygon.Messages;
using SpimexAdapter;
using SpimexAdapter.FTE;

namespace Polygon.Connector.Spimex
{
    internal class SpimexRouter : GatewayService, IOrderRouter, ITransactionVisitor
    {
        #region Fields

        private readonly InfoCommClient infoClient;
        private readonly TransCommClient transClient;

        private readonly IDictionary<string, InfoAccount> accounts = new Dictionary<string, InfoAccount>();

        private readonly Dictionary<int, InfoAccount> mapAccountIdToAccount = new Dictionary<int, InfoAccount>();

        #endregion
        
        public SpimexRouter(InfoCommClient infoClient, TransCommClient transClient)
        {
            this.infoClient = infoClient;
            this.transClient = transClient;

            infoClient.OnInfoOrder += InfoClient_OnInfoOrder;
            infoClient.OnInfoTrade += InfoClient_OnInfoTrade;
            infoClient.OnInfoHolding += InfoClient_OnInfoHolding;
            infoClient.OnInfoAccount += InfoClient_OnInfoAccount;

            infoClient.OnError += InfoClient_OnError;
            transClient.OnError += InfoClient_OnError;
        }

        #region Подписки

        #region Счета

        private void InfoClient_OnInfoAccount(InfoAccount account)
        {
            if (account.status == RowStatus.ACTIVE && account.type == AccountType.ACC_NORMAL)
            {
                accounts[account.code] = account;
                //firms.Add(account.firm);
                AvailableAccounts.Add(account.code);
            }
        }

        #endregion
        
        #region Заявки

        private void InfoClient_OnInfoOrder(InfoOrder order)
        {
            if (!accounts.ContainsKey(order.account))
            {
                return;
            }
            
            var activeQuantity = order.qtyLeft;

            OrderState state;
            switch (order.status)
            {
                case OrderStatus.CANCELED:
                    state = !string.IsNullOrEmpty(order.res_code) && order.res_code != "ENF_CANC" // TODO Hardcode волшебной строки
                        ? OrderState.Error
                        : OrderState.Cancelled;
                    activeQuantity = 0;
                    break;

                case OrderStatus.MATCHED:
                    state = OrderState.Filled;
                    break;

                case OrderStatus.FREEZED:
                case OrderStatus.QUEUED:
                    state = order.qty_executed > 0 ? OrderState.PartiallyFilled : OrderState.Active;
                    break;

                case OrderStatus.WAIT_APPROVAL:
                default:
                    return;
            }

            //long filledQty = order.ActiveQty - (long)infoOrder.qtyLeft;
            
            Guid.TryParseExact(order.trn, "N", out var trId);

            var oscm = new OrderStateChangeMessage
            {
                TransactionId = trId,
                OrderExchangeId = order.code,
                Quantity = order.qty,
                ActiveQuantity = activeQuantity,
                FilledQuantity = order.qty_executed, // TODO
                Price = PriceHelper.ToPrice(order.price),
                ChangeTime = DateTime.Now, // TODO
                State = state
            };

            OnMessageReceived(oscm);
        }

        #endregion


        #region Позиции

        private void InfoClient_OnInfoHolding(InfoHolding holding)
        {
            if (!accounts.ContainsKey(holding.account))
            {
                return;
            }

            if (!mapAccountIdToAccount.ContainsKey(holding.acc_id))
            {
                mapAccountIdToAccount.Add(holding.acc_id, accounts[holding.account]);
            }

            var position = new PositionMessage
            {
                Account = holding.account,
                Instrument = new Instrument { Code = holding.security + "|BRD-NORMAL" },
                Quantity = (int)holding.trade_buy_qty - (int)holding.trade_sell_qty,
                Price = PriceHelper.ToPrice(holding.settle_price)
            };

            OnMessageReceived(position);
        }

        #endregion


        #region Сделки

        private void InfoClient_OnInfoTrade(InfoTrade trade)
        {
            if (trade.acc_id == 0 || !mapAccountIdToAccount.ContainsKey(trade.acc_id))
            {
                return;
            }

            var fill = new FillMessage
            {
                Account = mapAccountIdToAccount[trade.acc_id].code,
                Instrument = new Instrument { Code = trade.security + "|" + trade.board },
                ExchangeId = trade.code,
                Operation = trade.buy_sell == BuySell.BUY ? OrderOperation.Buy : OrderOperation.Sell,
                Price = PriceHelper.ToPrice(trade.price),
                Quantity = trade.qty,
                DateTime = DateTime.FromFileTime((long)trade.trade_time),
                ExchangeOrderId = trade.order_id
            };

            OnMessageReceived(fill);
        }

        #endregion

        private void InfoClient_OnError(int code, string message)
        {
            if (SendErrorMessages)
            {
                OnMessageReceived(new ErrorMessage { Message = $"Code = {code}; Message = {message}" });
            }
        }

        #endregion

        #region OrderRouter

        public override void Start() { }

        public override void Stop() { }

        public List<string> AvailableAccounts { get; } = new List<string>();
        public bool IsPermittedAccount(string account) => AvailableAccounts.Contains(account);

        public void SendTransaction(Transaction transaction)
        {
            transaction.Accept(this);
        }

        public override void Dispose()
        {
            infoClient.OnInfoOrder -= InfoClient_OnInfoOrder;
            infoClient.OnInfoTrade -= InfoClient_OnInfoTrade;
            infoClient.OnInfoHolding -= InfoClient_OnInfoHolding;
            infoClient.OnInfoAccount -= InfoClient_OnInfoAccount;

            infoClient.OnError -= InfoClient_OnError;
            transClient.OnError -= InfoClient_OnError;
        }

        #endregion

        #region ITransactionVisitor

        public async void Visit(NewOrderTransaction transaction)
        {
            var parts = transaction.Instrument.Code.Split('|');
            
            if (!accounts.TryGetValue(transaction.Account, out var account))
            {
                OnMessageReceived(new TransactionReply
                {
                    TransactionId = transaction.TransactionId,
                    Message = $"Не найден счет {transaction.Account}",
                    Success = false
                });
                return;
            }

            var order = new SpimexAdapter.FTE.Order
            {
                account = account.code,
                firm = account.firm,
                client = account.client,

                security = parts[0],
                board = parts[1],

                @type = transaction.GetOrderType(),
                @params = transaction.GetOrderParams(),
                buy_sell = transaction.GetOperation(),

                price = PriceHelper.FromPrice(transaction.Price),
                qty = transaction.Quantity,
                trn = transaction.TransactionId.ToString("N"),
                isMarketMaker = transaction.IsMarketMakerOrder,
                comment = transaction.Comment,
            };


            try
            {
                var reply = await transClient.SendOrder(order);

                OnMessageReceived(new TransactionReply
                {
                    TransactionId = transaction.TransactionId,
                    Message = reply.code,
                    Success = true
                });

                OrderState? state = null;
                switch (reply.status)
                {
                    case OrderStatus.CANCELED:
                        state = OrderState.Cancelled;
                        break;

                    case OrderStatus.MATCHED:
                        state = OrderState.Filled;
                        break;

                    case OrderStatus.FREEZED:
                    case OrderStatus.QUEUED:
                        state = reply.qty_executed > 0 ? OrderState.PartiallyFilled : OrderState.Active;
                        break;
                }

                var oscm = new OrderStateChangeMessage
                {
                    TransactionId = transaction.TransactionId,
                    OrderExchangeId = reply.code,
                    Quantity = order.qty,
                    ActiveQuantity = reply.qtyLeft,
                    FilledQuantity = reply.qty_executed,
                    Price = PriceHelper.ToPrice(order.price),
                    ChangeTime = DateTime.Now, 
                    State = state
                };
                OnMessageReceived(oscm);
            }
            catch (FTEException e)
            {
                OnMessageReceived(new TransactionReply
                {
                    TransactionId = transaction.TransactionId,
                    Message = e.Message,
                    Success = false
                });
            }
        }

        public async void Visit(KillOrderTransaction transaction)
        {
            try
            {
                var reply = await transClient.CancelOrder(new CancelOrder { order_id = transaction.OrderExchangeId });

                OnMessageReceived(new TransactionReply
                {
                    TransactionId = transaction.TransactionId,
                    Message = reply.code,
                    Success = true
                });

                var oscm = new OrderStateChangeMessage
                {
                    TransactionId = transaction.TransactionId,
                    OrderExchangeId = reply.code,
                    Quantity = reply.qty,
                    ActiveQuantity = reply.qtyLeft,
                    FilledQuantity = reply.qty_executed,
                    ChangeTime = DateTime.Now,
                    State = OrderState.Cancelled
                };
                OnMessageReceived(oscm);
            }
            catch (FTEException e)
            {
                OnMessageReceived(new TransactionReply
                {
                    TransactionId = transaction.TransactionId,
                    Message = e.Message,
                    Success = false
                });
            }
        }

        public void Visit(ModifyOrderTransaction transaction)
        {
            OnMessageReceived(TransactionReply.Rejected(transaction, "NotSupported"));
        }

        public void Visit(Transaction transaction)
        {
            OnMessageReceived(TransactionReply.Rejected(transaction, "NotSupported"));
        }

        #endregion

    }
}
