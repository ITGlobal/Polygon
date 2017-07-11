using System;
using System.Collections.Concurrent;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    internal static class CQGCInstrumentParamsExtentions
    {
        // NOTE это какая-то подозрительная хуйня!
        private static readonly ConcurrentDictionary<Instrument, uint> decimalPlaces = new ConcurrentDictionary<Instrument, uint>();

        public static uint GetDecimalPlaces(this InstrumentParams ip)
        {
            if (ip.DecimalPlaces > 0)
                return ip.DecimalPlaces;

            if (ip.PriceStep <= 0)
                return 4;

            return decimalPlaces.GetOrAdd(ip.Instrument, _ =>
            {
                uint precision = 0;
                var x = ip.PriceStep;

                while (x * (decimal)Math.Pow(10, precision) != Math.Round(x * (decimal)Math.Pow(10, precision)))
                {
                    precision++;
                }

                return precision;
            });
        }
    }
}
