using System.Threading;
using JetBrains.Annotations;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    /// Утилита для ожидания отмены или продолжения задачи
    /// </summary>
    internal class ContinueOrCancelWaiter
    {
        #region fields

        /// <summary>
        /// Токен отмены задачи
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        /// Токен продолжения задачи
        /// </summary>
        private readonly WaitHandle continuationHandle;

        #endregion

        #region .ctor

        public ContinueOrCancelWaiter(CancellationToken cancellationToken, [NotNull] WaitHandle continuationHandle)
        {
            this.cancellationToken = cancellationToken;
            this.continuationHandle = continuationHandle;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// True если задачу нужно продолжать, false если дождались cancellationToken и задачу нужно завершить
        /// </summary>
        /// <returns></returns>
        public bool Wait()
        {
            var waited = new[] { cancellationToken.WaitHandle, continuationHandle };
            return WaitHandle.WaitAny(waited) != 0;
        }

        #endregion
    }
}