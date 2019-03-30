using System;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Connector.QUIKLua;
using Polygon.Messages;

namespace Polygon.Quik.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var quikParameters = new QLParameters(new QuikLocalInstrumentConverter())
            {
                IpAddress = args[0],
                Port = 1248
            };
            var connector = quikParameters.CreateConnector();
            connector.ConnectionStatusProviders[0].ConnectionStatusChanged += Program_ConnectionStatusChanged;
            connector.Feed.MessageReceived += Feed_MessageReceived;
            connector.Start();
            Console.ReadLine();
        }

        private static void Program_ConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            Console.WriteLine($"Connection status changed: {e.ConnectionStatus}");
        }

        private static void Feed_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"Msg received: {e.Message}");
        }
    }

    class QuikLocalInstrumentConverter : InstrumentConverter<InstrumentData>
    {
        protected override Task<Instrument> ResolveSymbolImplAsync(IInstrumentConverterContext<InstrumentData> context, string symbol, string dependentObjectDescription)
        {
            throw new NotImplementedException();
        }

        protected override Task<InstrumentData> ResolveInstrumentImplAsync(IInstrumentConverterContext<InstrumentData> context, Instrument instrument,
            bool isTestVendorCodeRequired)
        {
            return Task.FromResult(new InstrumentData() { Symbol = instrument.Code, Instrument = instrument });
        }
    }
}
