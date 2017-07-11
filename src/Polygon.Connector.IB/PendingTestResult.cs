using System.Threading.Tasks;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class PendingTestResult
    {
        private readonly TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

        public void Accept()
        {
            completionSource.SetResult(true);
        }

        public void Reject()
        {
            completionSource.SetResult(false);
        }

        public Task<bool> WaitAsync()
        {
            return completionSource.Task;
        }
    }
}

