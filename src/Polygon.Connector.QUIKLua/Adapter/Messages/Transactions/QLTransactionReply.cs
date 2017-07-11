using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages.Transactions
{
    [ObjectName(QLObjectNames.QLTransactionReply)]
    internal class QLTransactionReply : QLMessage
    {
        /// <summary>
        /// Пользовательский идентификатор транзакции
        /// </summary>
        public long trans_id { get; set; }

        /// <summary>
        /// Статус:
        /// 0  - транзакция отправлена серверу, 
        /// 1  - транзакция получена на сервер QUIK от клиента, 
        /// 2  - ошибка при передаче транзакции в торговую систему, поскольку отсутствует подключение шлюза Московской Биржи, повторно транзакция не отправляется, 
        /// 3  - транзакция выполнена, 
        /// 4  - транзакция не выполнена торговой системой, код ошибки торговой системы будет указан в поле «DESCRIPTION», 
        /// 5  - транзакция не прошла проверку сервера QUIK по каким-либо критериям. Например, проверку на наличие прав у пользователя на отправку транзакции данного типа, 
        /// 6  - транзакция не прошла проверку лимитов сервера QUIK, 
        /// 10 - транзакция не поддерживается торговой системой. К примеру, попытка отправить «ACTION = MOVE_ORDERS» на Московской Бирже, 
        /// 11 - транзакция не прошла проверку правильности электронной подписи. К примеру, если ключи, зарегистрированные на сервере, не соответствуют подписи отправленной транзакции. 
        /// 12 - не удалось дождаться ответа на транзакцию, т.к. истек таймаут ожидания. Может возникнуть при подаче транзакций из QPILE. 
        /// 13 - транзакция отвергнута, т.к. ее выполнение могло привести к кросс-сделке (т.е. сделке с тем же самым клиентским счетом).
        /// </summary>
        public byte status { get; set; }	
        
        /// <summary>
        /// Сообщение
        /// </summary>
        public string result_msg { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public long time { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public long uid { get; set; }	

        /// <summary>
        /// Флаги транзакции (временно не используется)
        /// </summary>
        public int flags { get; set; }	

        /// <summary>
        /// Идентификатор транзакции на сервере
        /// </summary>
        public long server_trans_id { get; set; }

        #region Nullable
        /// <summary>
        /// Номер заявки
        /// </summary>
        public long order_num { get; set; }	

        /// <summary>
        /// Цена
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int quantity { get; set; }		

        /// <summary>
        /// Остаток
        /// </summary>
        public int balance { get; set; }		

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public string firm_id { get; set; }

        /// <summary>
        /// Торговый счет
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Код клиента
        /// </summary>
        public string client_code { get; set; }

        /// <summary>
        /// Поручение
        /// </summary>
        public string brokerref { get; set; }

        /// <summary>
        /// Код класса
        /// </summary>
        public string class_code { get; set; }

        /// <summary>
        /// Код бумаги 
        /// </summary>
        public string sec_code { get; set; }

        #endregion

        public bool Successful
        {
            get
            {
                return status == 0 || status == 1 || status == 3;
               
            }
        }
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.TransactionId, trans_id);
            fmt.AddField(LogFieldNames.Status, status);
            fmt.AddField(LogFieldNames.Result, result_msg);
            fmt.AddField(LogFieldNames.Time, time);
            fmt.AddField(LogFieldNames.Uid, uid);
            fmt.AddField(LogFieldNames.ServerTransactionId, server_trans_id);
            return fmt.ToString();
        }
    }
}
