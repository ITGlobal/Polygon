using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Meta;

namespace SpimexAdapter
{
    public static class MessageParser
    {
        public static T Deserialize<T>(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }

        public static byte[] Serialize<T>(this T env)
            where T : IExtensible
        {
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<T>(ms, env);

                return ms.ToArray();
            }
        }
    }
}
