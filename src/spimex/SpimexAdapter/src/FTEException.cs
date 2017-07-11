using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    public class FTEException : Exception
    {
        public MsgType Type { get; }

        public FTEException(MsgType type)
            : base($"{type}: {type.GetErrorCode()}")
        {
            Type = type;
        }
    }
}
