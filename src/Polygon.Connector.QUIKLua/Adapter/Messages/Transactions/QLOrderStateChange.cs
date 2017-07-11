using System;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Newtonsoft.Json;
using Polygon.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages.Transactions
{
    [ObjectName(QLObjectNames.QLOrderStateChange)]
    internal class QLOrderStateChange : QLMessage, IEquatable<QLOrderStateChange>
    {
        [JsonIgnore]
        public OrderState State
        {
            /*             
                Флаг установлен	Значение
                бит 0 (0x1)	Заявка активна, иначе – не активна
                бит 1 (0x2)	Заявка снята. Если флаг не установлен и значение бита «0» равно «0», то заявка исполнена
                бит 2 (0x4)	Заявка на продажу, иначе – на покупку. Данный флаг для сделок и сделок для исполнения определяет направление сделки (BUY/SELL)
                бит 3 (0x8)	Заявка лимитированная, иначе – рыночная
                бит 4 (0x10)	Возможно исполнение заявки несколькими сделками
                бит 5 (0x20)	Исполнить заявку немедленно или снять (FILL OR KILL)
                бит 6 (0x40)	Заявка маркет-мейкера. Для адресных заявок – заявка отправлена контрагенту
                бит 7 (0x80)	Для адресных заявок – заявка получена от контрагента
                бит 8 (0x100)	Снять остаток
                бит 9 (0x200)	Айсберг-заявка
             */
            get
            {
                bool isActive = (flags & 0x1) != 0;
                bool isCancelled = (flags & 0x2) != 0;

                if (isActive)
                {
                    if (balance == qty)
                        return OrderState.Active;
                    
                    return OrderState.PartiallyFilled;
                }

                if (isCancelled)
                    return OrderState.Cancelled;

                return OrderState.Filled;
            }
        }

        [JsonIgnore]
        public OrderOperation Operation
        {
            /*             
                Флаг установлен	Значение
                бит 0 (0x1)	Заявка активна, иначе – не активна
                бит 1 (0x2)	Заявка снята. Если флаг не установлен и значение бита «0» равно «0», то заявка исполнена
                бит 2 (0x4)	Заявка на продажу, иначе – на покупку. Данный флаг для сделок и сделок для исполнения определяет направление сделки (BUY/SELL)
                бит 3 (0x8)	Заявка лимитированная, иначе – рыночная
                бит 4 (0x10)	Возможно исполнение заявки несколькими сделками
                бит 5 (0x20)	Исполнить заявку немедленно или снять (FILL OR KILL)
                бит 6 (0x40)	Заявка маркет-мейкера. Для адресных заявок – заявка отправлена контрагенту
                бит 7 (0x80)	Для адресных заявок – заявка получена от контрагента
                бит 8 (0x100)	Снять остаток
                бит 9 (0x200)	Айсберг-заявка
             */
            get { return (flags & 0x4) != 0 ? OrderOperation.Sell : OrderOperation.Buy; }
        }

        /// <summary>
        /// Номер заявки в торговой системе
        /// </summary>
        public long order_num { get; set; }

        /// <summary>
        /// Набор битовых флагов
        /// </summary>
        public int flags { get; set; }

        /// <summary>
        /// Комментарий, обычно: <код клиента>/<номер поручения>
        /// </summary>
        public string brokerref { get; set; }	

        /// <summary>
        /// Идентификатор трейдера
        /// </summary>
        public string userid { get; set; }	

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public string firmid { get; set; }	

        /// <summary>
        /// Торговый счет
        /// </summary>
        public string account { get; set; }	

        /// <summary>
        /// Цена
        /// </summary>
        public decimal price { get; set; }	

        /// <summary>
        /// Количество в лотах
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// Остаток
        /// </summary>
        public int balance { get; set; }	

        /// <summary>
        /// Исполненное количество
        /// </summary>
        public int filled { get; set; }

        /// <summary>
        /// Идентификатор транзакции
        /// </summary>
        public long trans_id { get; set; }	

        /// <summary>
        /// Код клиента
        /// </summary>
        public string client_code { get; set; }	

        /// <summary>
        /// Код биржи в торговой системе
        /// </summary>
        public string exchange_code { get; set; }	

        /// <summary>
        /// Время активации
        /// </summary>
        public long activation_time { get; set; }	

        /// <summary>
        /// Код бумаги заявки
        /// </summary>
        public string sec_code { get; set; }	

        /// <summary>
        /// Код класса заявки
        /// </summary>
        public string class_code { get; set; }

        /// <summary>
        /// Дата и время
        /// </summary>
        public QLDateTime datetime { get; set; }	

        /// <summary>
        /// Дата и время снятия заявки
        /// </summary>
        public QLDateTime withdraw_datetime { get; set; }	

        /// <summary>
        /// Способ указания объема заявки. Возможные значения:
        /// «0» – по количеству,
        /// «1» – по объему
        /// </summary>
        public byte value_entry_type { get; set; }	

        /// <summary>
        /// Причина отклонения заявки брокером
        /// </summary>
        public string reject_reason { get; set; }

        [JsonIgnore]
        public DateTime Time
        {
            get
            {
                var dt = new QLDateTime();

                if (State != OrderState.Cancelled)
                    dt = datetime;
                else
                    dt = withdraw_datetime;

                return new DateTime(dt.year, dt.month, dt.day, dt.hour, dt.min, dt.sec, DateTimeKind.Local);   
            }
        }

        #region IEquatable<QLOrderStateChange>
        
        public bool Equals(QLOrderStateChange other)
        {
            return other.order_num == order_num &&
                   other.brokerref == brokerref &&
                   other.userid == userid &&
                   other.firmid == firmid &&
                   other.account == account &&
                   other.price == price &&
                   other.qty == qty &&
                   other.balance == balance &&
                   other.trans_id == trans_id &&
                   other.client_code == client_code &&
                   other.exchange_code == exchange_code &&
                   other.activation_time == activation_time &&
                   other.sec_code == sec_code &&
                   other.class_code == class_code &&
                   other.datetime == datetime &&
                   other.withdraw_datetime == withdraw_datetime &&
                   other.value_entry_type == value_entry_type &&
                   other.reject_reason == reject_reason &&
                   other.flags == flags;
        }

        #endregion
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.TransactionId, trans_id);
            fmt.AddEnumField(LogFieldNames.State, State);
            fmt.AddField(LogFieldNames.OrderNumber, order_num);
            fmt.AddField(LogFieldNames.Flags, flags);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddField(LogFieldNames.BrokerRef, brokerref);
            fmt.AddField(LogFieldNames.UserId, userid);
            fmt.AddField(LogFieldNames.FirmId, firmid);
            fmt.AddField(LogFieldNames.Account, account);
            fmt.AddField(LogFieldNames.Price, price);
            fmt.AddField(LogFieldNames.Quantity, qty);
            fmt.AddField(LogFieldNames.Balance, balance);
            fmt.AddField(LogFieldNames.ClientCode, client_code);
            fmt.AddField(LogFieldNames.ExchangeCode, exchange_code);
            fmt.AddField(LogFieldNames.ActivationTime, activation_time);
            fmt.AddField(LogFieldNames.SecCode, sec_code);
            fmt.AddField(LogFieldNames.ClassCode, class_code);
            fmt.AddField(LogFieldNames.Time, datetime);
            fmt.AddField(LogFieldNames.WithdrawDatetime, withdraw_datetime);
            fmt.AddField(LogFieldNames.ValueEntryType, value_entry_type);
            fmt.AddField(LogFieldNames.RejectReason, reject_reason);
            return fmt.ToString();
        }
    }
}
