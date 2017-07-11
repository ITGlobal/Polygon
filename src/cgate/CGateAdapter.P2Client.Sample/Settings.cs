using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace CGateAdapter.P2Client.Sample
{
    public sealed class Settings
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("addr")]
        public string Address { get; set; }

        [JsonProperty("port")]
        [DefaultValue(4001)]
        public ushort Port { get; set; }

        [JsonProperty("data-login")]
        public string DataLogin { get; set; }

        [JsonProperty("data-pwd")]
        public string DataPassword { get; set; }

        [JsonProperty("trans-login")]
        public string TransactionLogin { get; set; }

        [JsonProperty("trans-pwd")]
        public string TransactionPassword { get; set; }

        public static Settings Load()
        {
            foreach (var filename in new[]{ "config.user.json", "config.json" })
            {
                if (!File.Exists(filename))
                {
                    continue;
                }

                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
            }

            throw new Exception("Unable to find matching config file");
        }
    }
}