using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Polygon.Connector.MicexBridge
{
    /// <summary>
    /// Settings for Micex Bridge API connector
    /// </summary>
    [PublicAPI]
    public class MicexBridgeConnectorSettings : IConnectorFactory
    {
        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="instrumentConverter"></param>
        /// <param name="interface">Идентификатор шлюзового интерфейса, с которым желает работать пользователь;</param>
        /// <param name="server">Имя сервера доступа торговой системы, например, «GATEWAY»</param>
        /// <param name="broadcast">Широковещательный адрес для поиска серверов доступа торговой системы, например «10.63.1.255,10.63.3.255,10.61.1.255,10.61.3.255»</param>
        /// <param name="userId">Идентификатор пользователя в торговой системе;</param>
        /// <param name="password">Пароль пользователя в торговой системе;</param>
        /// <param name="service">Имя сервиса сервера доступа, например, «gateway»</param>
        /// <param name="prefBroadcast">Адрес предпочтительного сервера доступа или сети.</param>
        /// <param name="boards">Список режимов, с которыми желает работать пользователь, например: “TQBR,TQOB,PSEQ”</param>
        /// <param name="cachefolder">Каталог для кеширования описания интерфейсов, загружаемых из торговой системы.</param>
        /// <param name="loglevel">Уровень внутреннего логирования транспортного протокола, 0-30</param>
        /// <param name="compression">Сжатие трафика, 0 - не сжимать, 1 - сжимать</param>
        /// <param name="ipsrcorder">Задает предпочтительные IP адреса источника</param>
        /// <param name="restrictlist">Поиск серверов доступа идет со всех доступных сетевых интерфейсов или только среди ipsrcorder</param>
        /// <param name="timeout">Время ожидания выполнения запроса сервером (торговой системой) в мс</param>
        /// <param name="logging">Строка в формате “N,M”, где первое число N – уровень логирования вызовов API MTESRL, M – уровень сбора статистики по соединению</param>
        /// <param name="retries">Количество попыток восстановить связь с ASTS Bridge Server в случае коммуникационных проблем. (по умолчанию 10);</param>
        /// <param name="connecttime">Максимальное время подключения или реконнекта в миллисекундах. По умолчанию 1 минута (60000). </param>
        /// <param name="logfolder">Каталог для создания лог-файлов работы библиотеки. По умолчанию лог-файлы создаются в одном каталоге с библиотекой</param>
        /// <param name="feedback">Текстовая строка в произвольном формате, описывающая клиентскую систему, подключающуюся к шлюзу. </param>
        public MicexBridgeConnectorSettings(InstrumentConverter<InstrumentData> instrumentConverter,
            string server,
            string service,
            string broadcast,
            string @interface,
            string userId,
            string password,
            string prefBroadcast= null,
            string boards= null,
            string cachefolder= null,
            int? loglevel=null,
            int? compression=null,
            string ipsrcorder= null,
            int? restrictlist=null,
            int? timeout=null,
            int? logging = null,
            int? retries = null,
            int? connecttime = null,
            string logfolder= null,
            string feedback= null)
        {
            InstrumentConverter = instrumentConverter;
            _server = server;
            _service = service;
            _broadcast = broadcast;
            _interface = @interface;
            _prefBroadcast = string.IsNullOrEmpty(prefBroadcast) ? broadcast : prefBroadcast;
            _userId = userId;
            _password = password;
            _boards = boards;
            _cachefolder = cachefolder;
            _loglevel = loglevel;
            _compression = compression;
            _ipsrcorder = ipsrcorder;
            _restrictlist = restrictlist;
            _timeout = timeout;
            _logging = logging;
            _retries = retries;
            _connecttime = connecttime;
            _logfolder = logfolder;
            _feedback = feedback;
        }

        #region Connection string parameters

        /// <summary>
        /// Имя сервера доступа торговой системы, например, «GATEWAY»
        /// </summary>
        [ConnectionStringProperty("SERVER", true)] private string _server;

        /// <summary>
        /// Имя сервиса сервера доступа, например, «gateway»
        /// </summary>
        [ConnectionStringProperty("SERVICE", true)] private string _service;

        /// <summary>
        /// Широковещательный адрес для поиска серверов доступа торговой системы, например «10.63.1.255,10.63.3.255,10.61.1.255,10.61.3.255»
        /// </summary>
        [ConnectionStringProperty("BROADCAST", true)] private string _broadcast;

        /// <summary>
        /// Идентификатор пользователя в торговой системе;
        /// </summary>
        [ConnectionStringProperty("USERID", true)] private string _userId;

        /// <summary>
        /// Пароль пользователя в торговой системе;
        /// </summary>
        [ConnectionStringProperty("PASSWORD", true)] private string _password;

        /// <summary>
        /// Идентификатор шлюзового интерфейса, с которым желает работать пользователь;
        /// </summary>
        [ConnectionStringProperty("INTERFACE", true)] private string _interface;

        /// <summary>
        /// Адрес предпочтительного сервера доступа или сети.
        /// </summary>
        [ConnectionStringProperty("PREFBROADCAST")] private string _prefBroadcast;

        /// <summary>
        /// Список режимов, с которыми желает работать пользователь, например: “TQBR,TQOB,PSEQ” 
        /// (необязательный параметр, если не задан будут выбраны все доступные режимы);
        /// </summary>
        [ConnectionStringProperty("BOARDS")] private string _boards;
        
        /// <summary>
        /// Каталог для кеширования описания интерфейсов, загружаемых из торговой системы.
        /// Если параметр не указан, кеширование не выполняется, а интерфейс скачивается из торговой системы при каждом подключении.
        /// </summary>
        [ConnectionStringProperty("CACHEFOLDER")] private string _cachefolder;

        /// <summary>
        /// Уровень внутреннего логирования транспортного протокола:
        /// “0” – логирование отключено(значение по умолчанию);
        ///     “1“ – “30“ – логирование включено, число задает подробность логирования.
        /// </summary>
        [ConnectionStringProperty("LOGLEVEL")] private int? _loglevel;

        /// <summary>
        /// сжатие трафика:
        ///    “0” – не сжимать данные;
        ///    “1” – сжимать данные(значение по умолчанию).
        /// </summary>
        [ConnectionStringProperty("COMPRESSION")] private int? _compression;

        /// <summary>
        /// Задает предпочтительные IP адреса источника (с каких сетевых интерфейсов следует устанавливать соединение). 
        /// Порядок адресов в перечислении используется для определения предпочтительного сервера. Если параметр RestrictList=0, 
        /// то выполняются попытки установить соединение и со всех остальных сетевых интерфейсов, не перечисленных в IpSrcOrder, 
        /// но с меньшим приоритетом. Если параметр RestrictList=1, то попытки установления соединения выполняются только с 
        /// указанных сетевых интерфейсов, например, “192.168.126.1, 192.168.56.1”.
        /// </summary>
        [ConnectionStringProperty("IPSRCORDER")] private string _ipsrcorder;

        /// <summary>
        /// “0“ - поиск серверов доступа идет со всех доступных сетевых интерфейсов (значение по умолчанию);
        /// “1“ - поиск идет только с тех интерфейсов, которые указаны в IpSrcOrder.
        /// </summary>
        [ConnectionStringProperty("RESTRICTLIST")] private int? _restrictlist;

        /// <summary>
        /// Время ожидания выполнения запроса сервером (торговой системой). Для шлюзовой mtesrl.dll – в миллисекундах, 
        /// для embedded mtesrl.dll ("встроенный" шлюз) - в секундах. Значение по умолчанию - 30 секунд. Если за указанное 
        /// время от сервера не будет получен ответ, начнется процедура реконнекта. Если обрыв связи будет диагностирован 
        /// раньше истечения таймаута, реконнект начнется раньше;
        /// </summary>
        [ConnectionStringProperty("TIMEOUT")] private int? _timeout;

        /// <summary>
        /// Строка в формате “N,M”, где первое число N – уровень логирования вызовов API MTESRL.
        ///   “0” отключить логирование операций(не создавать log-файл) “1” – логировать только ошибки
        ///   “2” – логировать все вызовы функция
        ///   “3” – логировать содержимое таблиц
        ///   “4” – логировать содержимое таблиц и номера полей
        ///   “5” - логировать сообщения транспортного протокола(только для встроенной версии библиотеки);
        ///   Второе число M – уровень сбора статистики по соединению.Статистика пишется в отдельный файл вида «mtesrl-YYYMMDD-<userid>-stats.log».
        ///   “0” – не собирать статистику;
        ///   “1” – собирать статистику по времена исполнения запросов и размеру ответов Торговой системы;
        ///   “2” - собирать статистику и распределение запросов по запросам таблицам.
        ///       По умолчанию включен уровень логирования 2,2.
        ///   Для полного отключения логирования необходимо задать строку “LOGGING= 0,0”
        ///   Срок хранения лог-файлов - 7 календарных дней. При вызове функции MTEConnect более старые файлы автоматически удаляются.
        /// </summary>
        [ConnectionStringProperty("LOGGING")] private int? _logging;

        /// <summary>
        /// Количество попыток восстановить связь с ASTS Bridge Server в случае коммуникационных проблем. (по умолчанию 10);
        /// </summary>
        [ConnectionStringProperty("RETRIES")] private int? _retries;

        /// <summary>
        /// Максимальное время подключения или реконнекта в миллисекундах. По умолчанию 1 минута (60000). 
        /// Может быть задано любое значение в диапазоне от 5 до 300 сек. Процедура реконнекта длится не более 
        /// RETRIES попыток и не дольше CONNECTTIME миллисекунд в зависимости от того, что наступит быстрее. 
        /// Значение данного таймаута является приблизительным и может отличаться от заданного на несколько секунд.
        /// </summary>
        [ConnectionStringProperty("CONNECTTIME")] private int? _connecttime;

        /// <summary>
        /// Каталог для создания лог-файлов работы библиотеки. По умолчанию лог-файлы создаются в одном каталоге с библиотекой
        /// </summary>
        [ConnectionStringProperty("LOGFOLDER")] private string _logfolder;

        /// <summary>
        /// Текстовая строка в произвольном формате, описывающая клиентскую систему, подключающуюся к шлюзу. 
        /// Например, «FondAnalytic v3.5.456, e-mail: admin@example.com».
        /// </summary>
        [ConnectionStringProperty("FEEDBACK")] private string _feedback;
        
        // TODO CryptParameters 
        //public CryptParameters CryptParams;

        #endregion
        
        /// <summary>
        /// Instrument ↔ Symbol converter
        /// </summary>
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }
        
        /// <inheritdoc />
        public IConnector CreateConnector()
        {
            return new MicexBridgeConnector(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //var field in GetType().GetFields().Where(_=>_.GetCustomAttributes<ConnectionStringPropertyAttribute>().Any())
            foreach (var field in GetType().GetFields(BindingFlags.NonPublic |
                                                      BindingFlags.Instance).Where(_=>_.GetCustomAttributes<ConnectionStringPropertyAttribute>().Any()))
            {
                var value = field.GetValue(this);
                var name = field.GetCustomAttribute<ConnectionStringPropertyAttribute>().Name;

                if (value != null)
                {
                    sb.AppendLine($"{name}={value}");
                    // var vType = value.GetType();
                    //if (vType == typeof(CryptParameters))
                    //    sb.Append(value.ToString());
                    //else if ((vType != typeof(int) || (int)value != -1))
                    //    sb.AppendFormat("{0}={1}\r\n", field.Name, value);
                }
            }

            return sb.ToString();
        }
    }
}
