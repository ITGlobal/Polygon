using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Messages;

namespace MicexBridge
{
    /// <summary>
    /// Implementation of resolving instrument ↔ symbol
    /// </summary>
    internal sealed class InstrumentSymbolResolver : InstrumentConverter<InstrumentData>
    {
        protected override Task<Instrument> ResolveSymbolImplAsync(IInstrumentConverterContext<InstrumentData> context, string symbol, string dependentObjectDescription)
        {
            return null;
        }

        protected override Task<InstrumentData> ResolveInstrumentImplAsync(IInstrumentConverterContext<InstrumentData> context, Instrument instrument,
            bool isTestVendorCodeRequired)
        {
            return null;
        }
    }
}
