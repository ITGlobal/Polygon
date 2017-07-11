using System;
using System.Threading;
using System.Threading.Tasks;

namespace Polygon.Connector
{
    internal static class TaskCompletionSourceExtensions
    {
        /// <summary>
        ///     Аналог <see cref="TaskCompletionSource{TResult}.TrySetResult"/> с фоновым исполнением continuation-ов
        /// </summary>
        public static void TrySetResultBackground<T>(this TaskCompletionSource<T> taskCompletionSource, T result)
        {
            ThreadPool.QueueUserWorkItem(_ => taskCompletionSource.TrySetResult(result));
        }

        /// <summary>
        ///     Аналог <see cref="TaskCompletionSource{T}.TrySetCanceled"/> с фоновым исполнением continuation-ов
        /// </summary>
        public static void TrySetCanceledBackground<T>(this TaskCompletionSource<T> taskCompletionSource)
        {
            ThreadPool.QueueUserWorkItem(_ => taskCompletionSource.TrySetCanceled());
        }

        /// <summary>
        ///     Аналог <see cref="TaskCompletionSource{TResult}.TrySetException(Exception)"/> с фоновым исполнением continuation-ов
        /// </summary>
        public static void TrySetExceptionBackground<T>(this TaskCompletionSource<T> taskCompletionSource, Exception exception)
        {
            ThreadPool.QueueUserWorkItem(_ => taskCompletionSource.TrySetException(exception));
        }

    }
}