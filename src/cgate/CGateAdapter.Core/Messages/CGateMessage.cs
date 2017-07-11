using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Базовый класс потоковых сообщений
    /// </summary>
    [PublicAPI, ProtoContract]
    public abstract partial class CGateMessage
    {
        /// <summary>
        ///     Название типа сообщения
        /// </summary>
        [ProtoIgnore]
        public abstract string MessageTypeName { get; }

        /// <summary>
        ///     Тип сообщения
        /// </summary>
        [ProtoIgnore]
        public abstract CGateMessageType MessageType { get; }

        /// <summary>
        ///     Поток, к которому относится сообщение
        /// </summary>
        [ProtoIgnore]
        public abstract CGateStreamType StreamType { get; }

        /// <summary>
        ///     Имя потока, из которого получено сообщение
        /// </summary>
        [ProtoMember(1)]
        public string StreamName { get; set; }

        /// <summary>
        ///     Состояние потока, в котором он находился, когда было получено сообщение
        /// </summary>
        [ProtoMember(2)]
        public StreamRegime StreamRegime { get; set; }

        /// <summary>
        ///     ext_id в ответных сообщениях на транзакции
        /// </summary>
        [ProtoMember(3)]
        public uint UserId { get; set; }

        /// <summary>
        ///     Индекс сообщения в моекс (оно же tableindex)
        /// </summary>
        [ProtoMember(4)]
        public uint MsgIndex { get; set; }

        /// <summary>
        ///     Посетитель
        /// </summary>
        public abstract void Accept(ICGateMessageVisitor visitor);
    }
}
