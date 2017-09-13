#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QuickFix.Fields;
using QuickFix.FIX44;
#endif
using Message = QuickFix.Message;

namespace Polygon.Connector.SpectraFix
{
    internal static class FixMessagePrinter
    {
#if !DEBUG
        public static string PrettyPrint(this Message msg)
        {
            return msg.ToString().Replace(Message.SOH, "|");
        }
#else
        public static string PrettyPrint(this Message msg)
        {
            var fields = ListFields(msg);
            var str = string.Join("; ", from f in fields select $"{f.Name}={f.Value}");
            return str;
        }

        private struct FixField
        {
            public string Name;
            public string Value;
        }

        private static IEnumerable<FixField> ListFields(Message msg)
        {
            var str = msg.ToString();
            var parts = str.Split(new[] { '\x01' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var i = part.IndexOf('=');
                if (i < 0)
                {
                    continue;
                }

                var name = part.Substring(0, i);
                var value = part.Substring(i + 1);

                int.TryParse(name, out var tag);
                if (_Fields.TryGetValue(tag, out var tagName))
                {
                    name = tagName;
                }

                if (_FieldFormatters.TryGetValue(tag, out var formatter))
                {
                    value = formatter(value);
                }

                yield return new FixField
                {
                    Name = name,
                    Value = value
                };
            }
        }

        private static readonly Dictionary<int, string> _Fields = new Dictionary<int, string>();
        private static readonly Dictionary<int, Func<string, string>> _FieldFormatters = new Dictionary<int, Func<string, string>>();

        static FixMessagePrinter()
        {
            _Fields.Add(1, "Account");
            _Fields.Add(6, "BeginAvgPxSeqNo");
            _Fields.Add(7, "BeginSeqNo");
            _Fields.Add(8, "BeginString");
            _Fields.Add(9, "BodyLength");
            _Fields.Add(10, "CheckSum");
            _Fields.Add(11, "ClOrdID");
            _Fields.Add(14, "CumQty");
            _Fields.Add(16, "EndSeqNo");
            _Fields.Add(17, "ExecID");
            _Fields.Add(31, "LastPx");
            _Fields.Add(32, "LastQty");
            _Fields.Add(34, "MsgSeqNum");
            _Fields.Add(35, "MsgType");
            _Fields.Add(37, "OrderID");
            _Fields.Add(38, "OrderQty");
            _Fields.Add(39, "OrdStatus");
            _Fields.Add(40, "OrdType");
            _Fields.Add(41, "OrigClOrdID");
            _Fields.Add(43, "PossDupFlag");
            _Fields.Add(44, "Price");
            _Fields.Add(45, "RefSeqNum");
            _Fields.Add(49, "SenderCompID");
            _Fields.Add(52, "SendingTime");
            _Fields.Add(54, "Side");
            _Fields.Add(55, "Symbol");
            _Fields.Add(56, "TargetCompID");
            _Fields.Add(58, "Text");
            _Fields.Add(59, "TimeInForce");
            _Fields.Add(60, "TransactTime");
            _Fields.Add(97, "PossResend");
            _Fields.Add(98, "EncryptMethod");
            _Fields.Add(102, "CxlRejReason");
            _Fields.Add(103, "OrdRejReason");
            _Fields.Add(108, "HeartBtInt");
            _Fields.Add(112, "TestReqID");
            _Fields.Add(122, "OrigSendingTime");
            _Fields.Add(123, "GapFillFlag");
            _Fields.Add(136, "NoMiscFees");
            _Fields.Add(141, "ResetSeqNumFlag");
            _Fields.Add(150, "ExecType");
            _Fields.Add(151, "LeavesQty");
            _Fields.Add(167, "SecurityType");
            _Fields.Add(198, "SecondaryOrderID");
            _Fields.Add(336, "TradingSessionID");
            _Fields.Add(371, "RefTagID");
            _Fields.Add(372, "RefMsgType");
            _Fields.Add(373, "SessionRejectReason");
            _Fields.Add(378, "ExecRestatementReason");
            _Fields.Add(432, "ExpireDate");
            _Fields.Add(434, "CxlRejResponseTo");
            _Fields.Add(448, "PartyID");
            _Fields.Add(452, "PartyRole");
            _Fields.Add(453, "NoPartyIDs");
            _Fields.Add(461, "CFICode");
            _Fields.Add(526, "SecondaryClOrdID");
            _Fields.Add(527, "SecondaryExecID");
            _Fields.Add(530, "MassCancelRequestType");
            _Fields.Add(531, "MassCancelResponse");
            _Fields.Add(532, "MassCancelRejectReason");
            _Fields.Add(533, "TotalAffectedOrders");
            _Fields.Add(555, "NoLegs");
            _Fields.Add(711, "NoUnderlyings");
            _Fields.Add(790, "OrdStatusReqID");
            _Fields.Add(880, "TrdMatchID");
            _Fields.Add(922, "EndCash");
            _Fields.Add(1300, "MarketSegmentID");
            _Fields.Add(20008, "Flags");
            _Fields.Add(20018, "Revision");
            _Fields.Add(20021, "MatchRef");

            _FieldFormatters.Add(35, MsgType);
            _FieldFormatters.Add(150, ChEnum<ExecType>());
            _FieldFormatters.Add(39, ChEnum<OrdStatus>());
            _FieldFormatters.Add(40, ChEnum<OrdType>());
            _FieldFormatters.Add(54, ChEnum<Side>());
            _FieldFormatters.Add(452, Enum<PartyRole>());
            _FieldFormatters.Add(59, ChEnum<TimeInForce>());

            string MsgType(string value)
            {
                switch (value)
                {
                    case Heartbeat.MsgType: return "Heartbeat";
                    case Reject.MsgType: return "Reject";
                    case Logon.MsgType: return "Logon";
                    case Logout.MsgType: return "Logout";
                    case ExecutionReport.MsgType: return "ExecutionReport";
                    case NewOrderSingle.MsgType: return "NewOrderSingle";
                    case OrderCancelReject.MsgType: return "OrderCancelReject";
                    case OrderCancelRequest.MsgType: return "OrderCancelRequest";
                    default: return value;
                }
            }

            Func<string, string> Enum<T>()
            {
                var consts = typeof(T)
#if NETSTANDARD1_6
                    .GetTypeInfo()
#endif
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(_ => _.FieldType == typeof(int));

                var names = new Dictionary<int, string>();
                foreach (var c in consts)
                {
                    var value = (int)c.GetValue(null);
                    var name = c.Name;
                    if (!names.ContainsKey(value))
                    {
                        names.Add(value, name);
                    }
                }

                return str =>
                {
                    if (int.TryParse(str, out var i) && names.TryGetValue(i, out var name))
                    {
                        return name;
                    }

                    return str;
                };
            }

            Func<string, string> ChEnum<T>()
            {
                var consts = typeof(T)
#if NETSTANDARD1_6
                    .GetTypeInfo()
#endif
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(_ => _.FieldType == typeof(char));

                var names = new Dictionary<char, string>();
                foreach (var c in consts)
                {
                    var value = (char)c.GetValue(null);
                    var name = c.Name;
                    if (!names.ContainsKey(value))
                    {
                        names.Add(value, name);
                    }
                }

                return str =>
                {
                    if (str.Length > 0 && names.TryGetValue(str[0], out var name))
                    {
                        return name;
                    }

                    return str;
                };
            }
        }
#endif
    }
}
