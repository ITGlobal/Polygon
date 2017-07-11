namespace Polygon.Connector.Spimex
{
    internal static class PriceHelper
    {
        private const double Coefficient = 0.01;

        public static decimal ToPrice(ulong value) => ToPrice((long)value);
        public static decimal ToPrice(long value) => (decimal)(value * Coefficient);

        public static long FromPrice(decimal value) => (long)(value / (decimal)Coefficient);
    }
}
