using System.Net;
using CGateAdapter;
using JetBrains.Annotations;

namespace Polygon.Connector.CGate
{
    /// <summary>
    ///     Настройки транспорта CGate
    /// </summary>
    [PublicAPI]
    public sealed class CGateParameters : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGateParameters(InstrumentConverter<InstrumentData> instrumentConverter) 
        {
            OrderBooksEnabled = true;
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///      Ключ для работы с плазой
        /// </summary>
        public string P2Key { get; set; }

        /// <summary>
        ///     Полный путь к каталогу хранения настроек
        /// </summary>
        public string ApplicationDataFolder { get; set; }

        /// <summary>
        ///     Если true, то осуществляется подключение к тестовому контуру
        /// </summary>
        public bool IsTestConnection { get; set; }

        /// <summary>
        ///     IP адрес, по которому слушает входящие подключения роутер plaza 2
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///     Порт, по которому слушает входящие подключения роутер plaza 2
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Если true, то загружаем стаканы
        /// </summary>
        public bool OrderBooksEnabled { get; set; }
        
        /// <summary>
        ///     Логин для конекшна данных
        /// </summary>
        public string DataConnectionLogin { get; set; }

        /// <summary>
        ///     Пароль для конекшна данных
        /// </summary>
        public string DataConnectionPassword { get; set; }

        /// <summary>
        ///     Логин для транзакционного конекшна
        /// </summary>
        public string TransactionConnectionLogin { get; set; }

        /// <summary>
        ///     Пароль для транзакционного конекшна
        /// </summary>
        public string TransactionConnectionPassword { get; set; }
        
        /// <summary>
        ///     Конвертер инструментов CGate
        /// </summary>
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }
        
        internal CGAdapterConfiguration ToCGAdapterConfiguration()
        {
            return new CGAdapterConfiguration
            {
                Address = IPAddress.Parse(IpAddress),
                Port = (ushort) Port,
                DataConnectionCredential = new NetworkCredential(DataConnectionLogin, DataConnectionPassword),
                TransactionConnectionCredential = new NetworkCredential(TransactionConnectionLogin, TransactionConnectionPassword),
            };
        }

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector()
        {
            return new CGateConnector(this, ApplicationDataFolder);
        } 
    }
}

