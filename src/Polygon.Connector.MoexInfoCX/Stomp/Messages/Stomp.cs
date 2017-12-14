using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Stomp : ClientMessage
    {
        public const string DEFAULT_VERSION = "1.2";

        public Stomp(
            string host,
            string login = null,
            string passcode = null,
            string acceptVersion = DEFAULT_VERSION)
            : base(StompCommands.STOMP)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host), $"\"host\" is required for {StompCommands.STOMP} frame");
            if (string.IsNullOrEmpty(acceptVersion))
                throw new ArgumentNullException(nameof(acceptVersion), $"\"accept-version\" is required for {StompCommands.STOMP} frame");

            SetHeader("host", host);
            SetHeader("accept-version", acceptVersion);
            SetHeader("login", login);
            SetHeader("passcode", passcode);
        }
    }
}