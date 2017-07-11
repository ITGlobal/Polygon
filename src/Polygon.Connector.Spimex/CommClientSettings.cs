using SpimexAdapter;

namespace Polygon.Connector.Spimex
{
    public class CommClientSettings : ICommClientSettings
    {
        public CommClientSettings(
            string username, 
            string password, 
            string iniFile, 
            string commonSectonName = null, 
            string connSectonName = null, 
            bool crypto = false,
            string logonInfo = null)
        {
            Username = username;
            Password = password;
            IniFile = iniFile;
            CommonSectonName = commonSectonName;
            ConnSectonName = connSectonName;
            Crypto = crypto;
            LogonInfo = logonInfo;
        }

        public string Username { get; }
        public string Password { get; }
        public string IniFile { get; }
        public string CommonSectonName { get; }
        public string ConnSectonName { get; }
        public bool Crypto { get; }
        public string LogonInfo { get; }
    }
}
