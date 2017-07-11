using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Состояние соединения было изменено
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CGConnectionStateChange : CGateMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGConnectionStateChange() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGConnectionStateChange(CGConnectionState connectionState)
        {
            ConnectionState = connectionState;
        }

        /// <summary>
        ///     Состояние соединения
        /// </summary>
        [ProtoMember(6)]
        public CGConnectionState ConnectionState { get; set; }

        /// <inheritdoc />
        public override string MessageTypeName => "CGConnectionStateChange";

        /// <inheritdoc />
        public override CGateMessageType MessageType => CGateMessageType.CGConnectionStateChange;

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Preudo;

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add("cstate", ConnectionState);
            return builder.ToString();
        }

        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }
}