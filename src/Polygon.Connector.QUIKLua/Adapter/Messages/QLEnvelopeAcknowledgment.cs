
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLEnvelopeAcknowledgment)]
    internal class QLEnvelopeAcknowledgment : QLMessage
    {
        public override QLMessageType message_type { get { return QLMessageType.EnvAck; } }

        /// <summary>
        /// Идентификатор полученного пакета
        /// </summary>
        public int id { get; set; }
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            return fmt.ToString();
        }
    }
}

