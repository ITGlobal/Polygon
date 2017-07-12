using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс для проверки подписки на инструмент по его коду
    /// </summary>
    [PublicAPI]
    public interface ISubscriptionTester<in T>
        where T : InstrumentData
    {
        /// <summary>
        ///     Проверить подписку 
        /// </summary>
        [NotNull]
        Task<SubscriptionTestResult> TestSubscriptionAsync(T data);
    }
}
