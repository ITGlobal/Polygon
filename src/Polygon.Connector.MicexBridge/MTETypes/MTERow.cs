#region

using System;
using System.Globalization;

#endregion

namespace Polygon.Connector.MicexBridge.MTETypes
{
    public class MTERow
    {
        public static readonly CultureInfo EnUsCultureInfo = new CultureInfo("en-Us");

        public string[] FieldData;

        public byte[] FieldNumbers;

        #region Index

        public string this[byte idx]
        {
            get
            {
                byte i = GetIndexByNumber(idx);
                if (i >= FieldData.Length) return null;
                else return FieldData[i];
            }
        }

        public int GetInt(byte idx)
        {
            return GetIntDirect(GetIndexByNumber(idx));
        }

        public long GetLong(byte idx)
        {
            return GetLongDirect(GetIndexByNumber(idx));
        }

        public double GetDouble(byte idx, int decimals)
        {
            return GetDoubleDirect(GetIndexByNumber(idx), decimals);
        }

        public decimal GetDecimal(byte idx, int decimals)
        {
            return GetDecimalDirect(GetIndexByNumber(idx), decimals);
        }

        public TimeSpan GetTimeSpan(byte idx)
        {
            return GetTimeSpanDirect(GetIndexByNumber(idx));
        }

        public DateTime GetDateTime(byte idx)
        {
            return GetDateTimeDirect(GetIndexByNumber(idx));
        }
        
        private byte GetIndexByNumber(byte number)
        {
            if (FieldNumbers.Length > number && FieldNumbers[number] == number)
            {
                return number;
            }

            for (byte i = 0; i < FieldNumbers.Length; i++)
            {
                if (FieldNumbers[i] == number)
                {
                    return i;
                }
            }

            return byte.MaxValue;
        }

        #endregion

        #region Direct

        public int GetIntDirect(byte idx)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data) ? 0 : int.Parse(data);
        }

        public long GetLongDirect(byte idx)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data) ? 0 : long.Parse(data);
        }

        public double GetDoubleDirect(byte idx, int decimals)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data) ? 0 : double.Parse(data) / decimals;
        }

        public decimal GetDecimalDirect(byte idx, int decimals)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data) ? 0 : decimal.Parse(data) / decimals;
        }

        public TimeSpan GetTimeSpanDirect(byte idx)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data)
                       ? TimeSpan.Zero
                       : DateTime.ParseExact(FieldData[idx], "HHmmss", EnUsCultureInfo).TimeOfDay;
        }

        public DateTime GetDateTimeDirect(byte idx)
        {
            var data = FieldData[idx];
            return string.IsNullOrEmpty(data)
                       ? DateTime.MinValue
                       : DateTime.ParseExact(FieldData[idx], "yyyyMMdd", EnUsCultureInfo);
        }

        #endregion
    }
}