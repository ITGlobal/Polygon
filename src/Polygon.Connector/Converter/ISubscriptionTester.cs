using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интефейс для проверки подписки на инструмент по вендорскому коду
    /// </summary>
    public interface ISubscriptionTester
    {
        /// <summary>
        ///     Проверить подписку на инструмент по вендорскому коду
        /// </summary>
        /// <param name="symbol">
        ///     Вендорский символ
        /// </param>
        /// <returns>
        ///     Асинхронный результат проверки
        /// </returns>
        [NotNull]
        Task<Tuple<bool, string>> TestVendorCodeAsync(string symbol);
    }
}
