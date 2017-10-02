using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Polygon.Connector.MicexBridge.Tests
{
    public class MicexBridgeConnectorSettingsTests
    {
        [Fact]
        public void ToStringWhenOnlyRequiredArgsProvided()
        {
            var settings = new MicexBridgeConnectorSettings(null, "127.0.0.2", "MCU0001", "777");
            var connectionString = settings.ToString();
            Assert.Equal(@"PASSWORD=777
SERVER=127.0.0.2
USERID=MCU0001
", connectionString);
        }
    }
}
