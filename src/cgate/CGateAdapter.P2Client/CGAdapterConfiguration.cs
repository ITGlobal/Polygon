using System.Net;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Параметры конфигурации для адаптера
    /// </summary>
    [PublicAPI]
    public sealed class CGAdapterConfiguration
    {
        /// <summary>
        ///     Ключ системы для работы с плазой, выдаётся по результатам сертификации
        /// </summary>
        public string Key { get; set; } = "";

        /// <summary>
        ///     IP-адрес раутера
        /// </summary>
        public IPAddress Address { get; set; }

        /// <summary>
        ///     Порт раутера
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        ///     Креды для data-подключения
        /// </summary>
        public NetworkCredential DataConnectionCredential { get; set; }

        /// <summary>
        ///     Креды для транзакционного подключения
        /// </summary>
        public NetworkCredential TransactionConnectionCredential { get; set; }

        /// <summary>
        ///     Логгер
        /// </summary>
        public ICGateLogger Logger { get; set; } = NullCGateLogger.Instance;

        private string _iniFolder;

        /// <summary>
        ///     Директория, в которой располагаются ini файла схемы потоков (по умолчанию не задаётся, файлы копируются в рабочий каталог программы)
        /// </summary>
        public string IniFolder
        {
            get => _iniFolder;
            set
            {
                if (value.EndsWith(@"\"))
                {
                    _iniFolder = value;
                }
                else
                {
                    _iniFolder = value + @"\";
                }
            }
        }
    }
}
