using System;
using System.Linq;
using System.Threading;
using Polygon.Diagnostics;
using ProtoBuf;
using System.Reflection;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Событие получения адаптером сообщения
    /// </summary>
    /// <typeparam name="T">
    ///     Тип сообщения
    /// </typeparam>
    internal sealed class AdapterEvent<T>
    {
        private readonly string messageName;

        private AdapterEventHandler<T> handler;

        public AdapterEvent()
        {
            messageName = typeof(T).GetTypeInfo().GetCustomAttributes(typeof(ProtoContractAttribute), false)
                .OfType<ProtoContractAttribute>()
                .Select(_ => _.Name)
                .FirstOrDefault() ?? typeof(T).Name;
        }

        /// <summary>
        ///     Добавить обработчик события
        /// </summary>
        public void Add(AdapterEventHandler<T> value)
        {
            var comparable = handler;
            AdapterEventHandler<T> comparand;

            do
            {
                comparand = comparable;
                comparable = Interlocked.CompareExchange(
                    ref handler,
                    (AdapterEventHandler<T>)Delegate.Combine(comparand, value),
                    comparand);

            } while (comparable != comparand);
        }

        /// <summary>
        ///     Удалить обработчик события
        /// </summary>
        public void Remove(AdapterEventHandler<T> value)
        {
            var comparable = handler;
            AdapterEventHandler<T> comparand;

            do
            {
                comparand = comparable;
                comparable = Interlocked.CompareExchange(
                    ref handler,
                    (AdapterEventHandler<T>)Delegate.Remove(comparand, value),
                    comparand);

            } while (comparable != comparand);
        }

        /// <summary>
        ///     Обработать сообщение
        /// </summary>
        public void Raise(T message) => RaiseImpl(message, hasRequestId: false);

        /// <summary>
        ///     Обработать сообщение
        /// </summary>
        public void Raise(uint requestId, T message) => RaiseImpl(message, requestId);

        /// <summary>
        ///     Обработать сообщение
        /// </summary>
        private void RaiseImpl(T message, uint requestId = 0, bool hasRequestId = true)
        {
            var h = handler;
            if (h != null)
            {
                try
                {
                    var args = new AdapterEventArgs<T>(message);

                    h(args);

                    if (args.Handled)
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    CQGCAdapter.Log.Error().Print(e, $"Failed to process a {messageName} message");
                    return;
                }
            }

            if (hasRequestId)
            {
                CQGCAdapter.Log.Warn().Print(
                    $"Received a {messageName} message but there are no consumers to handle it",
                    LogFields.RequestId(requestId)
                    );
            }
            else
            {
                CQGCAdapter.Log.Warn().Print($"Received a {messageName} message but there are no consumers to handle it");
            }
        }
    }
}

