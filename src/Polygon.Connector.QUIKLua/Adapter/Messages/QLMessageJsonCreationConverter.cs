using System;
using Polygon.Connector.QUIKLua.Adapter.Messages.Transactions;
using Newtonsoft.Json.Linq;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    class QLMessageJsonCreationConverter : JsonCreationConverter<QLMessage>
    {
        private static Type EnumType = typeof(QLMessageType);

        protected override QLMessage Create(Type objectType, JObject jObject)
        {
            if (FieldExists("message_type", jObject))
            {
                var type = jObject.GetValue("message_type").Value<string>();
                var qt = (QLMessageType) (Enum.Parse(EnumType, type));
                switch (qt)
                {
                    case QLMessageType.InstrumentParams:
                        return new QLInstrumentParams();
                    case QLMessageType.InstrumentsList:
                        return new QLInstrumentsList();
                    case QLMessageType.Heartbeat:
                        return new QLHeartbeat();
                    case QLMessageType.AccountsList:
                        return new QLAccountsList();
                    case QLMessageType.CandlesResponse:
                        return new QLHistoryDataResponse();
                    case QLMessageType.CandlesUpdate:
                        return new QLHistoryDataUpdate();
                    case QLMessageType.OrderBook:
                        return new QLOrderBook();
                    case QLMessageType.TransactionReply:
                        return new QLTransactionReply();
                    case QLMessageType.OrderStateChange:
                        return new QLOrderStateChange();
                    case QLMessageType.Position:
                        return new QLPosition();
                    case QLMessageType.MoneyPosition:
                        return new QLMoneyPosition();
                    case QLMessageType.Fill:
                        return new QLFill();
                    case QLMessageType.InitBegin:
                        return new QLInitBegin();
                    case QLMessageType.InitEnd:
                        return new QLInitEnd();
                }
            }

            return new QLUnknownMessage();
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}

