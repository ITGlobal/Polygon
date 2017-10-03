using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Polygon.Connector.MicexBridge.Tests
{
    public class MicexBridgeConnectorSettingsTests
    {
        /// <summary>
        /// Tests connection string formatting when only required arguments provided
        /// </summary>
        [Fact]
        public void ToStringWhenOnlyRequiredArgsProvided()
        {
            var settings = new MicexBridgeConnectorSettings(null, 
                "127.0.0.2",
                "the_service", 
                "127.0.0.3",
                "the_interface",
                "MCU0001", 
                "777");

            Assert.Equal(@"SERVER=127.0.0.2
SERVICE=the_service
BROADCAST=127.0.0.3
USERID=MCU0001
PASSWORD=777
PREFBROADCAST=127.0.0.3
", settings.ToString());
        }

        /// <summary>
        /// Tests connection string formatting when some optional argument provided
        /// </summary>
        [Fact]
        public void ToStringWhenBoardsProvided()
        {
            var settings = new MicexBridgeConnectorSettings(null,
                "127.0.0.2",
                "the_service",
                "127.0.0.3",
                "the_interface",
                "MCU0001",
                "777",
                boards: "TQBR,TQOB");

            Assert.Equal(@"SERVER=127.0.0.2
SERVICE=the_service
BROADCAST=127.0.0.3
USERID=MCU0001
PASSWORD=777
PREFBROADCAST=127.0.0.3
BOARDS=TQBR,TQOB
", settings.ToString());
        }

        /// <summary>
        /// Tests connection string formatting when some optional argument provided
        /// </summary>
        [Fact]
        public void ToStringWhenBoardsAndRetriesProvided()
        {
            var settings = new MicexBridgeConnectorSettings(null,
                "127.0.0.2",
                "the_service",
                "127.0.0.3",
                "the_interface",
                "MCU0001",
                "777",
                boards: "TQBR,TQOB",
                retries: 5);

            Assert.Equal(@"SERVER=127.0.0.2
SERVICE=the_service
BROADCAST=127.0.0.3
USERID=MCU0001
PASSWORD=777
PREFBROADCAST=127.0.0.3
BOARDS=TQBR,TQOB
RETRIES=5
", settings.ToString());
        }
    }
}
