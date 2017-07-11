using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Базовый класс фида.
    /// </summary>
    [PublicAPI]
    public abstract class Feed : GatewayService, IFeed {  }
}

