using System;

namespace Polygon.Connector.CQGContinuum
{
    internal sealed class InstrumentResolverEventArgs : EventArgs
    {
        public InstrumentResolverEventArgs(uint contractId)
        {
            ContractId = contractId;
        }

        public uint ContractId { get; set; }
    }
}