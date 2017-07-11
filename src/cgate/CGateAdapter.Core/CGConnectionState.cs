namespace CGateAdapter
{
    /// <summary>
    ///     Состояние соединения
    /// </summary>
    public enum CGConnectionState
    {
        /// <summary>
        ///     Нет соединения, адаптер требует перезапуска
        /// </summary>
        Shutdown,

        /// <summary>
        ///     Нет соединения, адаптер не требует перезапуска
        /// </summary>
        Disconnected,

        /// <summary>
        ///     Идет установка соединения
        /// </summary>
        Connecting,

        /// <summary>
        ///     Соединение установлено
        /// </summary>
        Connected
    }
}