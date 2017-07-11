using System;
using System.Threading;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    internal static class CancellationTokenExtensions
    {
        /// <summary>
        ///     Безопасная версия <see cref="CancellationToken.Register(System.Action)"/>.
        ///     В отличие от первообраза, она не падает, если токен уже стух
        /// </summary>
        public static CancellationTokenRegistration RegisterSafe(this CancellationToken token, [NotNull] Action callback)
        {
            try
            {
                if (!token.CanBeCanceled)
                {
                    return new CancellationTokenRegistration();
                }

                return token.Register(callback);
            }
            catch (ObjectDisposedException)
            {
                callback();
                return new CancellationTokenRegistration();
            }
        }
    }
}
