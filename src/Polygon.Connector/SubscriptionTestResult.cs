using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Результат проверки подписки на инструмент
    /// </summary>
    [PublicAPI]
    public sealed class SubscriptionTestResult
    {
        private static readonly SubscriptionTestResult _PassedValue = new SubscriptionTestResult(true, "");
        private static readonly SubscriptionTestResult _FailedValue = new SubscriptionTestResult(false, "");

        private SubscriptionTestResult(bool ok, string message)
        {
            Ok = ok;
            Message = message;
        }

        /// <summary>
        ///     Прошла ли проверка
        /// </summary>
        public bool Ok { get; }

        /// <summary>
        ///     Сообщение
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Создать значение, обозначающее успех
        /// </summary>
        public static SubscriptionTestResult Passed() => _PassedValue;

        /// <summary>
        ///     Создать значение, обозначающее успех
        /// </summary>
        public static SubscriptionTestResult Passed(string message) => new SubscriptionTestResult(true, message);

        /// <summary>
        ///     Создать значение, обозначающее неуспех
        /// </summary>
        public static SubscriptionTestResult Failed() => _FailedValue;

        /// <summary>
        ///     Создать значение, обозначающее неуспех
        /// </summary>
        public static SubscriptionTestResult Failed(string message) => new SubscriptionTestResult(false, message);
    }
}