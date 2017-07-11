using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Сообщение DataEnd
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CGateDataEnd : CGateMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateDataEnd() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateDataEnd(string streamName, StreamRegime streamRegime)
        {
            StreamName = streamName;
            StreamRegime = streamRegime;
        }

        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "CGateDataEnd";

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.CGateDataEnd;

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Preudo;

        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            return builder.ToString();
        }
    }
}