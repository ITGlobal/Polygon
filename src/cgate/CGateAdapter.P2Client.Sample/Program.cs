using System;
using System.IO;
using System.Net;
using System.Threading;
using CGateAdapter.Messages.FortsMessages;

namespace CGateAdapter.P2Client.Sample
{
    public static class Program
    {
        private static readonly object ConsoleLock = new object();

        private static CGP2ClientAdapter _adapter;
        private static CGateMessageHandler _cGateMessageHandler;

        public static void Main()
        {
            var settings = Settings.Load();

            var rootDir = Path.GetFullPath(".");
            while (!Directory.Exists(Path.Combine(rootDir, "ini")))
            {
                rootDir = Path.GetDirectoryName(rootDir);
            }

            var adapterConfig = new CGAdapterConfiguration
            {
                IniFolder = Path.Combine(rootDir, "scheme"),
                Address = IPAddress.Parse(settings.Address),
                Port = settings.Port,
                DataConnectionCredential = new NetworkCredential(
                    settings.DataLogin,
                    settings.DataPassword
                ),
                TransactionConnectionCredential = new NetworkCredential(
                    settings.TransactionLogin,
                    settings.TransactionPassword
                ),
                Logger = new Logger(),
                Key = settings.Key
            };

            _cGateMessageHandler = new CGateMessageHandler();
            _cGateMessageHandler.Print = Print;

            using (_adapter = new CGP2ClientAdapter(adapterConfig))
            {
                _adapter.ConnectionStateChanged += AdapterOnConnectionStateChanged;
                _adapter.MarketdataMessageReceived += AdapterOnMarketdataMessageReceived;
                _adapter.ExecutionMessageReceived += AdapterOnMarketdataMessageReceived;

                try
                {
                    _adapter.Start();

                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Escape)
                        {
                            _adapter.Stop();
                            break;
                        }

                        if (key.Key == ConsoleKey.P)
                        {
                            PlaceOrder();
                            //Task.Factory.StartNew(OrderPlacingTask);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                finally
                {
                    Console.WriteLine("Bye");
                }
            }
        }

        private static void AdapterOnMarketdataMessageReceived(object sender, CGateMessageEventArgs e)
        {
            e.Message.Accept(_cGateMessageHandler);
        }

        private static void AdapterOnConnectionStateChanged(object sender, CGConnectionStateEventArgs e)
        {
            Print($"Connection state changed to {e.ConnectionState}", ConsoleColor.Cyan);
        }

        private static CgmFutAddOrder _order;
        private static void OrderPlacingTask()
        {
            Print("Start order placing task", ConsoleColor.DarkCyan);

            while (true)
            {
                if (_order == null)
                {
                    _order = new CgmFutAddOrder
                    {
                        Isin = "SBRF-9.16",
                        Amount = 1,
                        Price = "12500",
                        BrokerCode = null,
                        ClientCode = "4100Y2B".Substring(4),
                        Dir = 1,
                        ExtId = IncrementOrderTransId(),
                        Type = 1
                    };

                    _adapter.SendMessage(_order);
                }
            }
        }

        private static CgmFutAddOrder PlaceOrder()
        {
            var newOrder = new CgmFutAddOrder
            {
                Isin = "SBRF-9.16",
                Amount = 1,
                Price = "12500",
                BrokerCode = null,
                ClientCode = "4100Y2B".Substring(4),
                Dir = 1,
                ExtId = IncrementOrderTransId(),
                Type = 1
            };

            _adapter.SendMessage(newOrder);

            return newOrder;
        }

        public static void Print(string text, ConsoleColor color)
        {
            lock (ConsoleLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }

        private static int _newTransactionId;
        private static int IncrementOrderTransId() => Interlocked.Increment(ref _newTransactionId);
    }
}
