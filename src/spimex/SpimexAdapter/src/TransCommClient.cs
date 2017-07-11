using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    public class TransCommClient : MessageCommClient
    {
        public TransCommClient(ICommClientSettings settings) 
            : base(settings)
        { }

        public async Task<OrderRepOK> SendOrder(Order order)
        {
            return await base.SendRequestAsync<OrderRepOK>(new FTEMessage(MsgType.REQ_ORDER, order));
        }

        public async Task<CancelOrderRepOK> CancelOrder(CancelOrder cancelOrder)
        {
            return await base.SendRequestAsync<CancelOrderRepOK>(new FTEMessage(MsgType.REQ_CANCEL_ORDER, cancelOrder));
        }
    }
}
