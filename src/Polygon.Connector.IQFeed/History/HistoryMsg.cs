using System;
using System.Diagnostics.CodeAnalysis;

namespace Polygon.Connector.IQFeed.History
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct HistoryMsg
    {
        private const int FIELD_TIME = 0;
        private const int FIELD_HIGH = 1;
        private const int FIELD_LOW = 2;
        private const int FIELD_OPEN = 3;
        private const int FIELD_CLOSE = 4;
        private const int FIELD_VOLUME = 5;
        private const int FIELD_VOLUME_HIT = 6;
        private const int FIELD_OPEN_INTEREST = 6;

        public DateTime Time;
        public decimal High;
        public decimal Low;
        public decimal Open;
        public decimal Close;
        public int Volume;
        public int OpenInterest;

        // Result Format for HIX, HID, and HIT requests: 
        // 
        // +---------------+---------------------+
        // | Field         | Format              |
        // +---------------+---------------------+
        // | RequestID     | Text                |
        // | Timestamp     | CCYY-MM-DD HH:MM:SS |
        // | High          | decimal             |
        // | Low           | decimal             |
        // | Open          | decimal             |
        // | Close         | decimal             |
        // | Total Volume  | integer             |
        // | Period Volume | integer             |
        // +---------------+---------------------+
        // 
        // 
        // Result Format for HDX, HDT, HWX, and HMX requests: 
        //
        // +---------------+---------------------+
        // | Field         | Format              |
        // +---------------+---------------------+
        // | RequestID     | Text                |
        // | Timestamp     | CCYY-MM-DD HH:MM:SS |
        // | High          | decimal             |
        // | Low           | decimal             |
        // | Open          | decimal             |
        // | Close         | decimal             |
        // | Period Volume | integer             |
        // | Open Interest | integer             |
        // +---------------+---------------------+

        public static void Parse(IQMessageArgs args, HistoryProviderSpan span, out HistoryMsg msg)
        {
            msg = new HistoryMsg();

            var isHitRequest = HistoryProviderSpan.Day > span;
            
            var fields = args.Message.Split(',');
            msg.Time = IQFeedParser.ParseDateTime(fields[FIELD_TIME], "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd");
            msg.Time = IQFeedParser.FromIQFeedTime(msg.Time) - span.ToTimeSpan(); // Переводим в нашу временную зону и сдвигаем все свечи на на величину периода, чтобы свечи были как везде
            msg.High = IQFeedParser.ParseDecimal(fields[FIELD_HIGH]);
            msg.Low = IQFeedParser.ParseDecimal(fields[FIELD_LOW]);
            msg.Open = IQFeedParser.ParseDecimal(fields[FIELD_OPEN]);
            msg.Close = IQFeedParser.ParseDecimal(fields[FIELD_CLOSE]);
            if (isHitRequest)
            {
                msg.Volume = IQFeedParser.ParseInt(fields[FIELD_VOLUME]);
                msg.OpenInterest = IQFeedParser.ParseInt(fields[FIELD_OPEN_INTEREST]);
            }
            else
            {
                msg.Volume = IQFeedParser.ParseInt(fields[FIELD_VOLUME_HIT]);
                msg.OpenInterest = 0;
            }
        }
    }
}

