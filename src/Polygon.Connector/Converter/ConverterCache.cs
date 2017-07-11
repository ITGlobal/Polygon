using System.Collections.Generic;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector
{
    /// <summary>
    ///     Абстрактный словарь с блокировкой для кэширования данных по инструментам для транспорта 
    /// </summary>
    /// <typeparam name="TKey">
    ///     Тип ключа
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     Тип значения
    /// </typeparam>
    internal abstract class ConverterCache<TKey, TValue>
    {
        private readonly ILockObject _cacheLock;
        private readonly Dictionary<TKey, TValue> _cache;

        protected ConverterCache()
        {
            _cacheLock = DeadlockMonitor.Cookie(GetType(), $"{GetType().ToString().ToLower()}Lock");
            _cache = new Dictionary<TKey, TValue>(); 
        }
        
        public TValue this[TKey index]
        {
            get
            {
                using (_cacheLock.Lock())
                {
                    TValue value;
                    return _cache.TryGetValue(index, out value) ? value : default(TValue);
                }
            }
        }

        public void UpdateCache(TKey key, TValue value)
        {
            using (_cacheLock.Lock())
            {
                if (_cache.ContainsKey(key))
                    Update(key, value);
                else
                    _cache.Add(key, value);
            }
        }

        private void Update(TKey key, TValue value)
        {
            _cache[key] = value;
        }
    }
}
