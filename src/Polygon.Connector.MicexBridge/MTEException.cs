#region

using System;
using Polygon.Connector.MicexBridge.MTETypes;

#endregion

namespace Polygon.Connector.MicexBridge
{
    public class MTEException : ApplicationException
    {
        private readonly ErrorCode errorCode;

        public MTEException(ErrorCode errorCode)
            : base("ErrCode = " + errorCode)
        {
            this.errorCode = errorCode;
        }

        public MTEException(ErrorCode errorCode, string message)
            : base(message + "\nErrCode = " + errorCode)
        {
            this.errorCode = errorCode;
        }

        public ErrorCode ErrorCode
        {
            get { return errorCode; }
        }

        //public override string Message
        //{
        //    get
        //    {
        //        return ToString();
        //    }
        //}

        public override string ToString()
        {
            return string.Format("{0}\nКод ошибки - {1}", Message, errorCode);
        }
    }
}