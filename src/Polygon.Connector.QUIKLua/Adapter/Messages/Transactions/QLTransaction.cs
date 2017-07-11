using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages.Transactions
{
    /// <summary>
    /// Транзакция - постановка новой заявки, kill, move
    /// </summary>
    [ObjectName(QLObjectNames.QLTransaction)]
    internal class QLTransaction : QLMessage
    {
        public override QLMessageType message_type { get { return QLMessageType.Transaction; } }

        /// <summary>
        /// Код класса, по которому выполняется транзакция, например TQBR. Обязательный параметр
        /// </summary>
        public string CLASSCODE { get; set; }
        
        /// <summary>
        /// Код инструмента, по которому выполняется транзакция, например SBER
        /// </summary>
        public string SECCODE { get; set; }

        /// <summary>
        /// Идентификатор участника торгов (код фирмы)
        /// </summary>
        public string FIRM_ID { get; set; }
        
        /// <summary>
        /// Номер счета Трейдера. Параметр обязателен при «ACTION» = «KILL_ALL_FUTURES_ORDERS». Параметр чувствителен к верхнему/нижнему регистру символов.
        /// </summary>
        public string ACCOUNT { get; set; }
        
        /// <summary>
        /// 20-ти символьное составное поле, может содержать код клиента и текстовый комментарий с тем же разделителем, что и при вводе заявки вручную. Параметр используется только для групповых транзакций. Необязательный параметр
        /// </summary>
        public string CLIENT_CODE { get; set; }
        
        /// <summary>
        /// Тип заявки, необязательный параметр. Значения: «L» – лимитированная (по умолчанию), «M» – рыночная
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TYPE TYPE { get; set; }

        /// <summary>
        /// Направление заявки, обязательный параметр. Значения: «S» – продать, «B» – купить
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OPERATION OPERATION { get; set; }
        
        /// <summary>
        /// Условие исполнения заявки, необязательный параметр. Возможные значения:
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EXECUTION_CONDITION EXECUTION_CONDITION { get; set; }
        
        /// <summary>
        /// Количество лотов в заявке, обязательный параметр
        /// </summary>
        public string QUANTITY { get; set; }
        
        /// <summary>
        /// Цена заявки, за единицу инструмента. Обязательный параметр. При выставлении рыночной заявки (TYPE=M) на Срочном рынке FORTS необходимо указывать значение цены – укажите наихудшую (минимально или максимально возможную – в зависимости от направленности), заявка все равно будет исполнена по рыночной цене. Для других рынков при выставлении рыночной заявки укажите price= 0.
        /// </summary>
        public string PRICE { get; set; }

        /// <summary>
        /// Номер заявки, снимаемой из торговой системы. Применяется при «ACTION» = «KILL_ORDER» или «ACTION» = «KILL_NEG_DEAL» или «ACTION» = «KILL_QUOTE»
        /// </summary>
        public string ORDER_KEY { get; set; }

        /// <summary>
        /// Уникальный идентификационный номер заявки, значение от 1 до 2 294 967 294
        /// </summary>
        public string TRANS_ID { get; set; }
        
        /// <summary>
        /// Текстовый комментарий, указанный в заявке. Используется при снятии группы заявок
        /// </summary>
        public string COMMENT { get; set; }
        
        /// <summary>
        /// Идентификатор базового контракта для фьючерсов или опционов. Обязательный параметр снятия заявок на рынке FORTS
        /// </summary>
        public string BASE_CONTRACT { get; set; }

        #region Modify transaction
        
        /// <summary>
        ///  Режим перестановки заявок на рынке FORTS. Параметр операции «ACTION» = «MOVE_ORDERS»
        /// </summary>
        public string MODE { get { return "1"; }}

        /// <summary>
        /// Биржевой номер заявки для изменения
        /// </summary>
        public string FIRST_ORDER_NUMBER { get; set; }

        /// <summary>
        /// Новое количество, на которое нужно изменить
        /// </summary>
        public string FIRST_ORDER_NEW_QUANTITY { get; set; }

        /// <summary>
        /// Новая цена, на которую нужно изменить
        /// </summary>
        public string FIRST_ORDER_NEW_PRICE { get; set; } 

        #endregion

        /// <summary>
        /// Вид транзакции
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ACTION ACTION { get; set; }
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.ClassCode, CLASSCODE);
            fmt.AddField(LogFieldNames.SecCode, SECCODE);
            fmt.AddField(LogFieldNames.FirmId, FIRM_ID);
            fmt.AddField(LogFieldNames.Account, ACCOUNT);
            fmt.AddField(LogFieldNames.ClientCode, CLIENT_CODE);
            fmt.AddEnumField(LogFieldNames.Type, TYPE);
            fmt.AddEnumField(LogFieldNames.Operation, OPERATION);
            fmt.AddEnumField(LogFieldNames.ExecutionCondition, EXECUTION_CONDITION);
            fmt.AddField(LogFieldNames.TransactionId, TRANS_ID);
            fmt.AddField(LogFieldNames.Comment, COMMENT);
            fmt.AddField(LogFieldNames.BaseContract, BASE_CONTRACT);
            fmt.AddEnumField(LogFieldNames.Action, ACTION);
            return fmt.ToString();
        }
    }
}
