using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polygon.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLFill)]
    internal class QLFill : QLMessage
    {
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
        /// Номер сделки в торговой системе
        /// </summary>
        public long trade_num { get; set; }

        /// <summary>
        /// Номер заявки в торговой системе
        /// </summary>
        public long order_num { get; set; }

        /// <summary>
        /// Комментарий, обычно: <код клиента>/<номер поручения>
        /// </summary>
        public string brokerref { get; set; }

        /// <summary>
        /// Идентификатор трейдера
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// Идентификатор дилера
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
        /// Количество бумаг в последней сделке в лотах
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// Набор битовых флагов
        /// </summary>
        public int flags { get; set; }

        /// <summary>
        /// Код клиента
        /// </summary>
        public string client_code { get; set; }

        /// <summary>
        /// 	Код бумаги заявки
        /// </summary>
        public string sec_code { get; set; }

        /// <summary>
        /// Код класса
        /// </summary>
        public string class_code { get; set; }

        /// <summary>
        /// Дата и время
        /// </summary>
        public QLDateTime datetime { get; set; }

        [JsonIgnore]
        public DateTime Time => new DateTime(datetime.year, datetime.month, datetime.day, datetime.hour, datetime.min, datetime.sec, DateTimeKind.Local);
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(Connector.LogFieldNames.TradeNum, trade_num);
            fmt.AddField(Connector.LogFieldNames.OrderNumber, order_num);
            fmt.AddField(Connector.LogFieldNames.BrokerRef, brokerref);
            fmt.AddField(Connector.LogFieldNames.UserId, userid);
            fmt.AddField(Connector.LogFieldNames.FirmId, firmid);
            fmt.AddField(Connector.LogFieldNames.Account, account);
            fmt.AddField(Connector.LogFieldNames.Price, price);
            fmt.AddField(Connector.LogFieldNames.Quantity, qty);
            fmt.AddField(Connector.LogFieldNames.Flags, flags);
            fmt.AddField(Connector.LogFieldNames.ClientCode, client_code);
            fmt.AddField(Connector.LogFieldNames.SecCode, sec_code);
            fmt.AddField(Connector.LogFieldNames.ClassCode, class_code);
            fmt.AddField(Connector.LogFieldNames.Time, datetime);
            return fmt.ToString();
        }
    }
}