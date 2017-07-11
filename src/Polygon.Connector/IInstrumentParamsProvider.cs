using System;
using JetBrains.Annotations;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Провайдер параметров инструмента
    /// </summary>
    [PublicAPI]
    public interface IInstrumentParamsProvider
    {
        /// <summary>
        ///     Активна ли подписка на параметры инструмента
        /// </summary>
        bool IsInstrumentParamSubscriptionActive { get; }

        /// <summary>
        ///  Текущие параметры инструмента
        /// </summary>
        [NotNull]
        InstrumentParams InstrumentParams { get; }

        /// <summary>
        ///     Бросается при изменении параметров инструмента
        /// </summary>
        event EventHandler<InstrumentParamsEventArgs> InstrumentParamsUpdated;

        /// <summary>
        ///     Событие обновления параметров инструмента без подписки на параметры инструмента
        /// </summary>
        event EventHandler InstrumentParamsUpdatedWeak;
    }
}

