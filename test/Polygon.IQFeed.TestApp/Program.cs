using System;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Connector.IQFeed;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.IQFeed.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new LocalInstrumentConverter();
            var iqParams = new IQFeedParameters(converter) { IQConnectAddress = "127.0.0.1" };
            var iqConnector = iqParams.CreateConnector();
            //LogManager.AddListener(this);

            iqConnector.Feed.MessageReceived += (_, e) =>
            {
                Console.WriteLine($"{e.Message}");
                //if (e.Message is InstrumentParams)
                //{
                //    Console.WriteLine("{@M}", e.Message);
                //}
            };

            iqConnector.ConnectionStatusProviders[0].ConnectionStatusChanged += (sender, arg) =>
            {

            };

            try
            {
                iqConnector.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed connect: {ex.Message}\n{ex.StackTrace}");
            }

            //var s2Provider = SymbolServiceInfrastructure.GetSymbolProvider();
            //var r2 = SymbolServiceInfrastructure.GetVendorCodeResolver();

            //var root = new TreeRoot(s2Provider, r2);

            //var exchanges = root.Exchanges;

            //var cme = exchanges.FirstOrDefault(_ => _.Code == "CME");
            //var assets = cme.Assets;
            //var es = assets.FirstOrDefault(_ => _.Code == "ES");
            //var futures = es.Futures;

            //foreach (var future in futures)
            //{
                var sr = iqConnector.InstrumentParamsSubscriber.Subscribe(new Instrument("@ESM18"));
                //Console.WriteLine(sr.Message);
            //}
            Console.ReadKey();
        }
    }

    class LocalInstrumentConverter : InstrumentConverter<IQFeedInstrumentData>
    {
        protected override Task<Instrument> ResolveSymbolImplAsync(
            IInstrumentConverterContext<IQFeedInstrumentData> context,
            string symbol,
            string dependentObjectDescription)
        {
            return Task.FromResult(new Instrument(symbol));
        }

        protected override Task<IQFeedInstrumentData> ResolveInstrumentImplAsync(
            IInstrumentConverterContext<IQFeedInstrumentData> context,
            Instrument instrument,
            bool isTestVendorCodeRequired)
        {
            return Task.FromResult(new IQFeedInstrumentData { Instrument = instrument, Symbol = instrument.Code });
        }
    }
}
