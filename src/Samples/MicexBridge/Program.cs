using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Connector.MicexBridge;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace MicexBridge
{
    /// <summary>
    /// Sample project demonstrating working with MicexBridgeConnector
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.AddListener(new Logger());

            try
            {
                var settings = new MicexBridgeConnectorSettings(new InstrumentSymbolResolver(),
                    "UAT_GATEWAY",//"",
                    "16411/16412",
                    "91.208.232.211",
                    "IFCBroker_17",
                    "MU9012400006",
                    "6334",
                    prefBroadcast: "91.208.232.211");

                var connector = settings.CreateConnector();
                //connector.Feed.MessageReceived += HandleFeedMessageReceived;
                //connector.ConnectionStatusProviders[0].ConnectionStatusChanged += HaneleConnectionStatusChanged;
                connector.Start();
                Console.WriteLine("MicexBridge connector started\nPress any key to stop");
                Console.ReadKey();
                connector.Stop();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}\n{exception.StackTrace}");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Handler for connection status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HaneleConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            Console.WriteLine($"Connection {e.ConnectionName} status: → {e.ConnectionStatus}");
        }

        /// <summary>
        /// Handler for processing a message from the datafeed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageReceivedEventArgs"></param>
        private static void HandleFeedMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            Console.WriteLine(messageReceivedEventArgs.Message);
        }
    }
}
