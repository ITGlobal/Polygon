using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Сообщение DataBegin
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CGateDataBegin : CGateMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateDataBegin() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateDataBegin(string streamName, StreamRegime streamRegime)
        {
            StreamName = streamName;
            StreamRegime = streamRegime;
        }

        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "CGateDataBegin";
        
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.CGateDataBegin;

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