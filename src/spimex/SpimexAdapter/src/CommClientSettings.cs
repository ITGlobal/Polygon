namespace SpimexAdapter
{
    public interface ICommClientSettings
    {
        string Username { get; }
        string Password { get; }
        string IniFile { get; }
        string CommonSectonName { get; }
        string ConnSectonName { get; }
        bool Crypto { get; }
        string LogonInfo { get; }
    }
}
