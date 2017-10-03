namespace Polygon.Connector.MicexBridge
{
    /// <summary>
    /// Типы секций ММВБ.
    /// </summary>
    public enum MicexSecionType
    {
        /// <summary>
        /// Фондовый рынок.
        /// </summary>
        Stock=0,

        /// <summary>
        /// Срочный рынок.
        /// </summary>
        Derivatives,

        /// <summary>
        /// Валютный рынок.
        /// </summary>
        Currency
    }
}