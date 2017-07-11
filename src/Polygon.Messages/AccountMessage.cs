using System;
using JetBrains.Annotations;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщения по конкретному счету.
    /// </summary>
    [Serializable]
    [PublicAPI]
    public abstract class AccountMessage : Message
    {
        /// <summary>
        ///     Номер счета.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     Код клиента.
        /// </summary>
        public string ClientCode { get; set; }
    }
}

