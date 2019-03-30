using System;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using JetBrains.Annotations;
using Polygon.Messages;
using IBOrder = IBApi.Order;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Отвечает за отправку транзакций
    /// </summary>
    internal sealed class TransactionDispatcher : ITransactionVisitor
    {
        private static readonly ILog _Log = LogManager.GetLogger<TransactionDispatcher>();
        private readonly IBConnector connector;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public TransactionDispatcher(IBConnector connector)
        {
            this.connector = connector;
        }

        public void Send(Transaction transaction)
        {
            try
            {
                transaction.Accept(this);
            }
            catch (TransactionRejectedException exception) 
            {
                Reject(transaction, exception.Message);
            }
            catch (Exception exception)
            {
                _Log.Error()
                    .Print(exception, "Unable to send transaction", LogFields.Transaction(transaction),
                        LogFields.Message(exception.Message));
                Reject(transaction, "Transaction execution failed: {0}", exception.Message);
            }
        }

        #region ITransactionVisitor

        /// <summary>
        ///     Обработать транзакцию <see cref="NewOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        async void ITransactionVisitor.Visit(NewOrderTransaction transaction)
        {
            try
            {
                var order = new IBOrder
                {
                    Account = transaction.Account,
                    ClientId = connector.ClientId,
                    Action = transaction.Operation == OrderOperation.Buy ? "BUY" : "SELL",
                    TotalQuantity = (int) transaction.Quantity,
                    OrderRef = !string.IsNullOrWhiteSpace(transaction.Comment)
                        ? connector.IBOrderRouter.SessionUidInternal + transaction.Comment
                        : connector.IBOrderRouter.SessionUidInternal,
                    Transmit = true
                };

                switch (transaction.Type)
                {
                    case OrderType.Limit:
                        order.OrderType = "LMT";
                        order.LmtPrice = (double) transaction.Price;
                        break;

                    case OrderType.Market:
                        order.OrderType = "MKT";
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("transaction.Type");
                }

                switch (transaction.ExecutionCondition)
                {
                    case OrderExecutionCondition.PutInQueue:
                        order.Tif = "GTC"; /* Good till cancelled */
                        break;
                    case OrderExecutionCondition.FillOrKill:
                        order.Tif = "FOC"; /* Fill or cancel */
                        break;
                    case OrderExecutionCondition.KillBalance:
                        order.Tif = "IOC"; /* Immediate or cancel */
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("transaction.ExecutionCondition");
                }

                await connector.Adapter.PlaceOrderAsync(transaction, order);
            }
            catch (TransactionRejectedException exception)
            {
                Reject(transaction, exception.Message);
            }
            catch (Exception exception)
            {
                _Log.Error()
                    .Print(exception, "Unable to send transaction", LogFields.Transaction(transaction),
                        LogFields.Message(exception.Message));
                Reject(transaction, "Transaction execution failed: {0}", exception.Message);
            }
        }

        /// <summary>
        ///     Обработать транзакцию <see cref="ModifyOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(ModifyOrderTransaction transaction)
        {
            TransactionIsNotSupported(transaction);
        }

        /// <summary>
        ///     Обработать транзакцию <see cref="KillOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(KillOrderTransaction transaction)
        {
            // Разбираем permId из OrderExchangeId
            int permId;
            if (!int.TryParse(transaction.OrderExchangeId, out permId))
            {
                Reject(transaction, "Invalid order exchange ID: \"{0}\".", transaction.OrderExchangeId);
                return;
            }

            connector.Adapter.KillOrder(permId, transaction);
        }

        /// <summary>
        ///     Обработать прочие транзакции
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(Transaction transaction)
        {
            TransactionIsNotSupported(transaction);
        }

        #endregion

        [StringFormatMethod("message")]
        private void Reject(Transaction transaction, string message, params object[] args)
        {
            var reply = new TransactionReply
            {
                Success = false,
                TransactionId = transaction.TransactionId,
                Message = string.Format(message, args)
            };
            connector.IBOrderRouter.Transmit(reply);
        }

        private void TransactionIsNotSupported<T>(T transaction)
            where T : Transaction
        {
            Reject(transaction, "Transaction {0} is not supported", typeof(T).Name);
        }
    }
}

