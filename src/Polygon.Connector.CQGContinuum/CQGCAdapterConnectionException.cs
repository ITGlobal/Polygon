using System;
using JetBrains.Annotations;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    /// Исключение сведетельствующее об ошибке подключения адаптера CQGC
    /// </summary>
    [Serializable, PublicAPI]
    public class CQGCAdapterConnectionException : Exception
    {
        public CQGCAdapterConnectionException()
        {
            
        }

        public CQGCAdapterConnectionException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        public CQGCAdapterConnectionException(string message)
            : base(message)
        {
        }
    }
}

