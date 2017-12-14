using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Connect : ClientMessage
    {
        public const string DEFAULT_VERSION = "1.2";

        public const string RECEIPT = "connect";

        public Connect(
            string host,
            string login = null,
            string passcode = null,
            string domain = null,
            string acceptVersion = DEFAULT_VERSION)
            : base(StompCommands.CONNECT)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host), $"\"host\" is required for {StompCommands.CONNECT} frame");
            if (string.IsNullOrEmpty(acceptVersion))
                throw new ArgumentNullException(nameof(acceptVersion), $"\"accept-version\" is required for {StompCommands.CONNECT} frame");

            SetHeader("host", host);
            SetHeader("accept-version", acceptVersion);
            SetHeader("login", login);
            SetHeader("passcode", passcode);

            if (!string.IsNullOrEmpty(domain))
                SetHeader("domain", domain);

            SetHeader("receipt", RECEIPT);

            //language:ru
        }
    }
}
