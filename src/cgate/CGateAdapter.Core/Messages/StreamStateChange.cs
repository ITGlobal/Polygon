using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Сообщение об изменении состояния потока
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class StreamStateChange : CGateMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public StreamStateChange() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public StreamStateChange(CGateStreamType affectedStreamType, string streamName, StreamRegime streamRegime)
        {
            AffectedStreamType = affectedStreamType;
            StreamName = streamName;
            StreamRegime = streamRegime;
        }

        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "StreamStateChange";

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.StreamStateChange;

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Preudo;

        /// <summary>
        ///     Тип изменившегося потока
        /// </summary>
        [ProtoMember(9)]
        public CGateStreamType AffectedStreamType { get; set; }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add("affected_str", AffectedStreamType);
            return builder.ToString();
        }

        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }
}
