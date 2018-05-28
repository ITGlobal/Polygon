using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Connector.CQGContinuum;
using Polygon.Messages;

namespace Polygon.CQG.TestApp
{
    internal class InstrumentConverterCQG : InstrumentConverter<InstrumentData>
    {
        protected override Task<Instrument> ResolveSymbolImplAsync(IInstrumentConverterContext<InstrumentData> context, string symbol, string dependentObjectDescription)
        {
            throw new NotImplementedException();
        }

        protected override Task<InstrumentData> ResolveInstrumentImplAsync(IInstrumentConverterContext<InstrumentData> context, Instrument instrument,
            bool isTestVendorCodeRequired)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ic = new InstrumentConverterCQG();
            var parameters = new CQGCParameters(ic)
            {
                ConnectionUrl = "wss://api.cqg.com",
                Username = "86155599",
                Password = "Global#1"
            };
            var connector = parameters.CreateConnector();
            connector.Router.MessageReceived += Router_MessageReceived;
            connector.ConnectionStatusProviders[0].ConnectionStatusChanged += Program_ConnectionStatusChanged;
            connector.Feed.MessageReceived += Feed_MessageReceived;
            connector.Start();
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        private static void Feed_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"FM: {e.Message}");
        }

        private static void Program_ConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            Console.WriteLine($"CS: {e.ConnectionStatus}");
        }

        private static void Router_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"RM: {e.Message}");
        }
    }
}
