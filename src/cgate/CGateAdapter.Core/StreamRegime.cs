using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Режим потока
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum StreamRegime
    {
        /// <summary>
        ///     Снапшот
        /// </summary>
        SNAPSHOT,

        /// <summary>
        ///     Онлайн-режим
        /// </summary>
        ONLINE,
        
        /// <summary>
        ///     Поток закрыт
        /// </summary>
        CLOSED
    }
}