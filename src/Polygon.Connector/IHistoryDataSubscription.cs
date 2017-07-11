using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Подписка на исторические данные
    /// </summary>
    [PublicAPI]
    public interface IHistoryDataSubscription : IDisposable { }
}

