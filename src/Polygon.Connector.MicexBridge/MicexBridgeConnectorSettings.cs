using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Polygon.Connector.MicexBridge
{
    /// <summary>
    /// Settings for Micex Bridge API connector
    /// </summary>
    [PublicAPI]
    public class MicexBridgeConnectorSettings : IConnectorFactory
    {
        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="instrumentConverter"></param>
        /// <param name="server"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public MicexBridgeConnectorSettings(InstrumentConverter<InstrumentData> instrumentConverter,
            string server,
            string userId,
            string password)
        {
            InstrumentConverter = instrumentConverter;
            _server = server;
            _userId = userId;
            _password = password;
        }

        [ConnectionStringProperty("BOARDS")] private string _boards;

        [ConnectionStringProperty("CONNECTTIME")] private int? _connecttime;
        
        [ConnectionStringProperty("FEEDBACK")] private string _feedback;

        [ConnectionStringProperty("HOST")] private string _host;

        [ConnectionStringProperty("INTERFACE")] private string _interface;

        [ConnectionStringProperty("LOGFOLDER")] private string _logfolder;

        [ConnectionStringProperty("LOGGING")] private int? _logging;

        [ConnectionStringProperty("PASSWORD")] private string _password;

        [ConnectionStringProperty("PROFILENAME")] private string _profilename;

        [ConnectionStringProperty("RETRIES")] private int? _retries;

        [ConnectionStringProperty("SERVER")] private string _server;

        [ConnectionStringProperty("SYNCTIME")] private int? _synctime;

        [ConnectionStringProperty("USERID")] private string _userId;

        [ConnectionStringProperty("TIMEOUT")] private int? _timeout;

        [ConnectionStringProperty("PORT")] private string _port;

        // TODO CryptParameters 
        //public CryptParameters CryptParams;

        // TODO delete?
        //public int BAUDRATE = 460800;

        /// <summary>
        /// Instrument ↔ Symbol converter
        /// </summary>
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }
        
        /// <inheritdoc />
        public IConnector CreateConnector()
        {
            return new MicexBridgeConnector(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //var field in GetType().GetFields().Where(_=>_.GetCustomAttributes<ConnectionStringPropertyAttribute>().Any())
            foreach (var field in GetType().GetFields(BindingFlags.NonPublic |
                                                      BindingFlags.Instance).Where(_=>_.GetCustomAttributes<ConnectionStringPropertyAttribute>().Any()))
            {
                var value = field.GetValue(this);
                var name = field.GetCustomAttribute<ConnectionStringPropertyAttribute>().Name;

                if (value != null)
                {
                    sb.AppendLine($"{name}={value}");
                    // var vType = value.GetType();
                    //if (vType == typeof(CryptParameters))
                    //    sb.Append(value.ToString());
                    //else if ((vType != typeof(int) || (int)value != -1))
                    //    sb.AppendFormat("{0}={1}\r\n", field.Name, value);
                }
            }

            return sb.ToString();
        }
    }
}
