using QuickFix;

namespace Polygon.Connector.SpectraFix
{
    /// <summary>
    ///     Параметры для соединения со Spectra FIX
    /// </summary>
    public sealed class SpectraFixSettings : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public SpectraFixSettings(InstrumentConverter<InstrumentData> instrumentConverter)
        {
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///     Конвертер инструментов
        /// </summary>
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }

        /// <summary>
        ///     BeginString
        /// </summary>
        public string BeginString { get; set; } = "FIX.4.4";

        /// <summary>
        ///     Адрес FIX Шлюза
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Порт FIX шлюза
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     SenderCompID
        /// </summary>
        public string SenderCompId { get; set; }

        /// <summary>
        ///     TargetCompID
        /// </summary>
        public string TargetCompId { get; set; }

        /// <summary>
        ///     Интервал передачи Heartbeat сообщений 
        /// </summary>
        public int HeartbeatInterval { get; set; } = 60;

        /// <summary>
        ///     Создать транспорт
        /// </summary>
        public IConnector CreateConnector() => new SpectraFixConnector(this);

        internal SessionSettings CreateSessionSettings()
        {
            var sessionSettings = new Dictionary();
            sessionSettings.SetString("ConnectionType", "initiator");
            sessionSettings.SetLong("ReconnectInterval", 60);
            sessionSettings.SetLong("HeartBtInt", HeartbeatInterval);

            // sessionSettings.SetString("DefaultApplVerID", "");

            Resources.DeployDataDictionaries();
            sessionSettings.SetString("TransportDataDictionary", Resources.FIX44);
            //sessionSettings.SetString("AppDataDictionary", Resources.FIX44);
            sessionSettings.SetString("DataDictionary", Resources.FIX44);
            sessionSettings.SetString("StartTime", "00:00:00");
            sessionSettings.SetString("EndTime", "00:00:00");
            sessionSettings.SetBool("UseDataDictionary", false);
            sessionSettings.SetBool("ResetOnLogon", true);


            sessionSettings.SetString("BeginString", BeginString);
            sessionSettings.SetString("TargetCompID", TargetCompId);
            sessionSettings.SetString("SenderCompID", SenderCompId);
            sessionSettings.SetString("SocketConnectHost", Address);
            sessionSettings.SetLong("SocketConnectPort", Port);

            var session = new SessionID(BeginString, SenderCompId, TargetCompId);

            var settings = new SessionSettings();
            settings.Set(session, sessionSettings);
            return settings;
        }
    }
}