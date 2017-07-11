using System;
using System.Collections.Generic;
using System.Threading;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using JetBrains.Annotations;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Потокобезопасный контейнер объектов
    /// </summary>
    /// <typeparam name="TKey">
    ///     Тип ключа
    /// </typeparam>
    /// <typeparam name="TObject">
    ///     Тип объекта
    /// </typeparam>
    internal sealed class ThreadSafeContainer<TKey, TObject>
    {
        private readonly IRwLockObject itemsLock = DeadlockMonitor.ReaderWriterLock<ThreadSafeContainer<TKey, TObject>>("itemsLock");
        private readonly Dictionary<TKey, TObject> items = new Dictionary<TKey, TObject>();
        [CanBeNull]
        private readonly Func<TKey, TObject> defaultFactory;

        public ThreadSafeContainer(Func<TKey, TObject> defaultFactory = null)
        {
            this.defaultFactory = defaultFactory;
        }

        /// <summary>
        ///     Получить объект по ключу
        /// </summary>
        /// <param name="key">
        ///     Ключ
        /// </param>
        /// <param name="factory">
        ///     Фабрика объекта
        /// </param>
        /// <returns>
        ///     Объект
        /// </returns>
        public TObject Get(TKey key, Func<TKey, TObject> factory = null)
        {
            using (itemsLock.UpgradableReadLock())
            {
                TObject result;
                if (!items.TryGetValue(key, out result))
                {
                    if (factory == null && defaultFactory == null)
                    {
                        throw new InvalidOperationException("Both factory and defaultFactory are not set");
                    }
                    
                    result = (factory ?? defaultFactory)(key);
                    using (itemsLock.WriteLock())
                    {
                        items.Add(key, result);
                    }
                }

                return result;
            }
        }
    }
}

