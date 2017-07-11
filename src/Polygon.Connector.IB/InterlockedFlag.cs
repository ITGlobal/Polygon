using System.Threading;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class InterlockedFlag
    {
        private const int True = 1;
        private const int False = 0;

        private int value;

        public bool IsSet => Interlocked.CompareExchange(ref value, False, False) == True;


        public void Set() => SetValue(True);
        public void Reset() => SetValue(False);

        private void SetValue(int x)
        {
            var y = value;

            while (true)
            {
                var z = Interlocked.CompareExchange(ref value, x, y);
                if (z == y)
                {
                    break;
                }

                y = z;
            }
        }
    }
}