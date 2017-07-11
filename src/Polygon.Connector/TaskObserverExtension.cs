using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    internal static class TaskObserverExtension
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(TaskObserverExtension));

        /// <summary>
        ///     Этот метод нужно для того, чтобы избежать warning-а CS4014
        /// </summary>
        public static void Ignore(this Task task, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            task.ContinueWith(_ =>
            {
                if (_.IsFaulted)
                {
                    _Log.Error(callerLineNumber, callerMemberName).Print(_.Exception, $"Async operation failure in {callerMemberName}:{callerLineNumber}");
                }
            });
        }
    }
}