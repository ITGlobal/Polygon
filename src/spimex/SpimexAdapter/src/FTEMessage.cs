using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    public class FTEMessage
    {
        public FTEMessage(MsgType type, IExtensible message)
            :this(type, message.Serialize())
        { }

        public FTEMessage(MsgType type, byte[] body = null)
        {
            Type = type;
            Body = body;
        }

        public MsgType Type { get; }
        public byte[] Body { get; }

        public bool HasData => Body != null;

        public bool IsReplyOk => Type == MsgType.REP_OK;

        public T GetMessage<T>()
            where T : class, IExtensible
        {
            return HasData ? MessageParser.Deserialize<T>(Body) : null;
        } 
    }


}
