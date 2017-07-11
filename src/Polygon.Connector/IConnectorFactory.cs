using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Фабрика для создания транспорта
    /// </summary>
    [PublicAPI]
    public interface IConnectorFactory
    {
        /// <summary>
        ///     Создать транспорт
        /// </summary>
        [NotNull]
        IConnector CreateConnector();
    }
}