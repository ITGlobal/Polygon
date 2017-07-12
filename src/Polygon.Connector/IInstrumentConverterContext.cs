using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Контекст для <see cref="InstrumentConverter{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     Тип метаданных инструмента
    /// </typeparam>
    [PublicAPI]
    public interface IInstrumentConverterContext<in T>
        where T : InstrumentData
    {
        /// <summary>
        ///     Интерфейс для проверки подписки на инструмент по его коду
        /// </summary>
        [CanBeNull]
        ISubscriptionTester<T> SubscriptionTester { get; }
    }
}