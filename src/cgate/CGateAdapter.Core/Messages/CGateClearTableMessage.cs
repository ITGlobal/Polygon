using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Сообщение MsgP2Repl_ClearDeleted
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CGateClearTableMessage : CGateMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateClearTableMessage() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateClearTableMessage(uint tableIdx, long tableRev, CGateStreamType streamType, string streamName, StreamRegime streamRegime)
        {
            TableIdx = tableIdx;
            TableRev = tableRev;
            StreamType = streamType;
            StreamName = streamName;
            StreamRegime = streamRegime;
        }

        /// <summary>
        ///     table_idx
        /// </summary>
        [ProtoMember(7)]
        public uint TableIdx { get; set; }

        /// <summary>
        ///     table_rev
        /// </summary>
        [ProtoMember(8)]
        public long TableRev { get; set; }
        
        /// <summary>
        ///     Название типа сообщения
        /// </summary>
        [ProtoIgnore]
        public override string MessageTypeName => "ClearTableMessage";

        /// <summary>
        ///     Тип сообщения
        /// </summary>
        public override CGateMessageType MessageType => CGateMessageType.CGateClearTableMessage;

        /// <summary>
        ///     Поток, к которому относится сообщение
        /// </summary>
        public override CGateStreamType StreamType { get; }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add("tableIdx", TableIdx);
            builder.Add("tableRev", TableRev);

            return builder.ToString();
        }

        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }
}