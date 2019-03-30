using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector.InteractiveBrokers;
using IBApi;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Контейнер заявок
    /// </summary>
    internal sealed class OrderInfoContainer
    {
        private readonly IRwLockObject containerLock = DeadlockMonitor.ReaderWriterLock<OrderInfoContainer>("containerLock");
        private readonly Dictionary<int, OrderInfo> ordersByTickerId = new Dictionary<int, OrderInfo>();
        private readonly Dictionary<int, OrderInfo> ordersByPermId = new Dictionary<int, OrderInfo>();

        private readonly HashSet<int> pendingTransactions = new HashSet<int>();
        private readonly Dictionary<int, PendingTestResult> pendingFillByPermId = new Dictionary<int, PendingTestResult>();

        public void StoreByTickerId(int tickerId, OrderInfo order, bool addPendingTransaction = false)
        {
            using (containerLock.WriteLock())
            {
                ordersByTickerId[tickerId] = order;

                // При отправке заявки из адаптера, добавляем транзакцию в ожидаемые
                if (addPendingTransaction)
                {
                    pendingTransactions.Add(tickerId);
                }
            }
        }

        public bool TryGetByTickerId(int tickerId, out OrderInfo order)
        {
            using (containerLock.ReadLock())
            {
                return ordersByTickerId.TryGetValue(tickerId, out order);
            }
        }

        public void StoreByPermId(int permId, OrderInfo order)
        {
            using (containerLock.WriteLock())
            {
                ordersByPermId[permId] = order;
            }
        }

        public bool TryGetByPermId(int permId, out OrderInfo order)
        {
            using (containerLock.ReadLock())
            {
                return ordersByPermId.TryGetValue(permId, out order);
            }
        }


        /// <summary>
        ///     Попытка отправить ожидающие филы при приходе ордера
        /// </summary>
        public void ProcessPendingFills(OrderInfo order)
        {
            using (containerLock.ReadLock())
            {
                // Если какие-то сделки ожидают эту заявку, их можно отправлять
                PendingTestResult pendingResult;
                if (pendingFillByPermId.TryGetValue(order.PermId, out pendingResult))
                {
                    pendingResult.Accept();
                    pendingFillByPermId.Remove(order.PermId);
                }

                if (pendingTransactions.Contains(order.OrderId))
                {
                    // Убираем из ожидаемых транзакций
                    pendingTransactions.Remove(order.OrderId);

                    // Если больше нет ожидаемых транзакций, можно отправить все ожидающие сделки
                    if (!pendingTransactions.Any() && pendingFillByPermId.Any())
                    {
                        foreach (var pendingFill in pendingFillByPermId.Values)
                        {
                            pendingFill.Accept();
                        }

                        pendingFillByPermId.Clear();
                    }
                }
            }
        }

        /// <summary>
        ///     Подождать заявку перед отправкой сделки
        /// </summary>
        public async Task WaitOrderForFill(Execution fill)
        {
            PendingTestResult pendingResult;

            using (containerLock.ReadLock())
            {
                // Если заявка уже пришла или не ждём транзакций, то сделку можно отправлять сразу
                if (ordersByPermId.ContainsKey(fill.PermId) || !pendingTransactions.Any())
                {
                    return;
                }

                if (!pendingFillByPermId.TryGetValue(fill.PermId, out pendingResult))
                {
                    pendingFillByPermId[fill.PermId] = pendingResult = new PendingTestResult();
                }
            }

            // Иначе ждём информации по заявке
            await pendingResult.WaitAsync();
        }
    }
}

