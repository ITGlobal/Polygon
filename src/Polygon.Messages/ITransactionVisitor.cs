using JetBrains.Annotations;

namespace Polygon.Messages
{
    /// <summary>
    ///     Посетитель для иерархии классов <see cref="Transaction"/>
    /// </summary>
    [PublicAPI]
    public interface ITransactionVisitor
    {
        /// <summary>
        ///     Обработать транзакцию <see cref="NewOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void Visit(NewOrderTransaction transaction);

        /// <summary>
        ///     Обработать транзакцию <see cref="ModifyOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void Visit(ModifyOrderTransaction transaction);

        /// <summary>
        ///     Обработать транзакцию <see cref="KillOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void Visit(KillOrderTransaction transaction);

        /// <summary>
        ///     Обработать прочие транзакции
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void Visit(Transaction transaction);
    }
}

