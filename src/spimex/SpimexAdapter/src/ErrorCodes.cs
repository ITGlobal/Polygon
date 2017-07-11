using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    static class ErrorCodes
    {
        public static string GetErrorCode(this MsgType type)
        {
            switch (type)
            {
                default:
                case MsgType.REQ_UNKNOWN:
                    return string.Empty;

	            /* Requests */
	            case MsgType.REQ_NEW_PARTICIPANT					: return "Запрос на создание нового участника";
	            case MsgType.REQ_NEW_USER						: return "Запрос на создание нового пользователя";
                case MsgType.REQ_NEW_CLIENT						: return "Запрос на создание нового клиента";
                case MsgType.REQ_NEW_ACCOUNT						: return "Запрос на создание нового счета";
                case MsgType.REQ_NEW_SECTION						: return "Запрос на создание новой секции";
                case MsgType.REQ_NEW_BOARD						: return "Запрос на создание нового режима торгов";
                case MsgType.REQ_NEW_SECTYPE						: return "Запрос на создание нового типа срочного контракта";
                case MsgType.REQ_NEW_SECURITY					: return "Запрос на создание нового срочного контракта";

                case MsgType.REQ_LOGON							: return "Регистрация в информационной или транзакционной подсистеме";
                case MsgType.REQ_LOGOFF							: return "Завершение работы в информационной или транзакционной подсистеме";

                case MsgType.REQ_ORDER							: return "Посылка заявки";
                case MsgType.REQ_CANCEL_ORDER					: return "Запрос на сняние заявки";

                case MsgType.REQ_OPEN_SECBOARD					: return "Открытие режима торгов";
                case MsgType.REQ_CLOSE_SECBOARD					: return "Закрытие режима торгов";
                case MsgType.REQ_SUSPEND_SECBOARD				: return "Приостановка режима торгов";
                case MsgType.REQ_RESUME_SECBOARD					: return "Продолжение нормального функционирования режима торгов";

                case MsgType.REQ_SUBSCRIBE						: return "Подпись на определенную информационную таблицу";
                case MsgType.REQ_UNSUBSCRIBE						: return "Запрос на завершение получения данных по определенной информационной таблицы";

                case MsgType.REQ_ORDER_ALARM						: return "Сообщение о необычной заявке";
                case MsgType.REQ_TRADE_ALARM						: return "Сообщение о необычной сделке";

                case MsgType.REQ_CLOSE_DAY						: return "Запрос на закрытие торгового дня";

                case MsgType.REQ_SUSPEND_PARTICIPANT				: return "Блокировка участника";
                case MsgType.REQ_RESUME_PARTICIPANT				: return "Разблокировка участника";

                case MsgType.REQ_SUSPEND_USER					: return "Блокировка пользователя";
                case MsgType.REQ_RESUME_USER						: return "Разблокировка пользователя";

                case MsgType.REQ_SUSPEND_CLIENT					: return "Блокировка клиента";
                case MsgType.REQ_RESUME_CLIENT					: return "Разблокировка клиента";

                case MsgType.REQ_SUSPEND_ACCOUNT					: return "Блокировка регистра";
                case MsgType.REQ_RESUME_ACCOUNT					: return "Разбокировка регистра";

                case MsgType.REQ_SUSPEND_SECTION					: return "Блокировка секции";
                case MsgType.REQ_RESUME_SECTION					: return "Разблокировка секции";

                case MsgType.REQ_MEDDELANDE						: return "Посылки текстового сообщения";

                case MsgType.REQ_CLR_TRADE						: return "Запрос на регистрацию сделки в клиринговой системе";
                case MsgType.REQ_CLR_SETTLE_PRICE				: return "Запрос на регистрацию новой расчетной цены в клиринговой системе";

                case MsgType.REQ_ACCEPT_ORDER					: return "Посылка аксепта из клиринговой системы";

                case MsgType.REQ_CLR_ORDER_REESTR				: return "Запрос на пересылку в клиринговую систему реестра заявок";
                case MsgType.REQ_CLR_TRADE_REESTR				: return "Запрос на пересылку в клиринговую систему реестра сделок";

                case MsgType.REQ_CLR_ORDER_CANC_REESTR			: return "Запрос на пересылку в клиринговую систему реестра снятых заявок";

                case MsgType.REQ_BRD_SECURITIES					: return "Запрос на подготовку данных для открыти торгов по серии срочного контракта";
                case MsgType.REQ_OPEN_BOARD						: return "Запрос на открытия для торгов режима торгов";
                case MsgType.REQ_CLOSE_BOARD_FOR_ORDERS			: return "Запрос на закрытие приема заявок и \"заморозка\" принятых заявокдля режима торгов";
                case MsgType.REQ_CLOSE_BOARD						: return "Запрос на закрытие режима торгов";
                case MsgType.REQ_HOLDING							: return "Запрос на передачу данных по холдингу из клиринговой системы";
                case MsgType.REQ_BD_STATE						: return "Зарезервировано. В текущей версии не используется";
                case MsgType.REQ_OPEN_BOARD_FOR_ORDERS			: return "Запрос на повторное открытие режима торгов для приема заявок";

                case MsgType.REQ_CLR_ORDER_REESTR_RFQ			: return "Запрос на пересылку в клиринговую систему реестра заявок RFQ";
                case MsgType.REQ_CLR_TRADE_REESTR_RFQ			: return "Запрос на пересылку в клиринговую систему реестра сделок RFQ";
                case MsgType.REQ_CLR_ORDER_REESTR_DELI			: return "Запрос на пересылку в клиринговую систему реестра заявок на физ. исполнение";
                case MsgType.REQ_CLR_TRADE_REESTR_DELI			: return "Запрос на пересылку в клиринговую систему реестра сделок на физ. исполнение";
                case MsgType.REQ_CLR_ORDER_REESTR_SPEC			: return "Запрос на пересылку в клиринговую систему реестра заявок специальной торговой сессии";
                case MsgType.REQ_CLR_TRADE_REESTR_SPEC			: return "Запрос на пересылку в клиринговую систему реестра сделок специальной торговой сессии";

                case MsgType.REQ_CLR_ORDER						: return "Запрос на проверку заявки в клиринговой системе";
                case MsgType.REQ_CLR_ORDER_RFQ					: return "Запрос на проверку заявки во время ликвидационной сессии";
                case MsgType.REQ_CLR_ORDER_DELI					: return "Запрос на проверку заявки на физическое исполнение";
                case MsgType.REQ_CLR_ORDER_SPEC					: return "Запрос на проверку заявки во время специальной сессии";

                case MsgType.REQ_CLR_DELI_MATCH					: return "Запрос на регистрацию сделки во время предпоставочной сессии";

                case MsgType.REQ_CLR_TRADE_RFQ					: return "Запрос на регистрацию сделки в клиринговой системе во время RFQ сессии";
                case MsgType.REQ_CLR_TRADE_DELI					: return "Запрос на регистрацию сделки на физическую поставку в клиринговой системе";
                case MsgType.REQ_CLR_TRADE_SPEC					: return "Запрос на регистрацию сделки в клиринговой системе во воремя специальной сессии";

                case MsgType.REQ_BD_SCHEDULE_UPDATE				: return "Запрос на изменение расписания бизнес-дня";

                case MsgType.REQ_FORCE_LOGOFF					: return "Принудительное завершение работы в информационной или транзакционной подсистеме";

                case MsgType.REQ_CHANGE_PASSWORD					: return "Запрос на изменение пароля";

                case MsgType.REQ_CANC_ORDERS_BY_PARTICIPANT		: return "Запрос на снятие всех заявок партисипанта";
                case MsgType.REQ_CANC_ORDERS_BY_USER				: return "Запрос на снятие всех заявок пользователя";
                case MsgType.REQ_CANC_ORDERS_BY_CLIENT			: return "Запрос на снятие всех заявок клиента";
                case MsgType.REQ_CANC_ORDERS_BY_ACCOUNT			: return "Запрос на снятие всех заявок по счету";
                case MsgType.REQ_CANC_ORDERS_BY_SECURITY			: return "Запрос на снятие всех заявок по инструменту";
                case MsgType.REQ_CANC_ORDERS_BY_BOARD			: return "Запрос на снятие всех заявок по борду";
                case MsgType.REQ_CANC_ORDERS_BY_SECBOARD			: return "Запрос на снятие всех заявок по секборду";

                case MsgType.REQ_UPDATE_USER_ACC_RIGHTS			: return "Запрос на изменения прав пользователя для счетов";
                case MsgType.REQ_UPDATE_USER_ACC_RIGHTS_OVER		: return "Запрос на изменения прав пользователя для счета";

                case MsgType.REQ_UPDATE_USER_SECBOARD_RIGHTS		: return "Запрос на изменения прав пользователя для секборда";

                case MsgType.REQ_FORCE_LOGOFF_EX					: return "Принудительное завершение работы в информационной или транзакционной подсистеме";

                case MsgType.REQ_CHANGE_SECBOARD_STATUS			: return "Изменение сосотояния режима торгов вместо( REQ_CLOSE_SECBOARD, REQ_SUSPEND_SECBOARD, REQ_RESUME_SECBOARD )";

                case MsgType.REQ_EXCHANGE_RATE					: return "Запрос на изменение курса валют";

                case MsgType.REQ_SERVER_TIME                     : return "Запрос времени сервера";
                case MsgType.REQ_ORDER_TRADE_ID					: return "стартовые номера";

                case MsgType.REQ_TABS_PARAMS						: return "Запрос на пересылку параметров внутренних таблиц";
                case MsgType.REQ_CALC_MARKET_PRICE				: return "Запрос на вычисление расчетной цены";


                case MsgType.REQ_SERIE_ACCOUNTS_LIST				: return "Список \"Котируемых серии\"";

                case MsgType.REQ_SUSPEND_SESSION					: return "Блокировка сессии";
                case MsgType.REQ_RESUME_SESSION					: return "Разбокировка сессии";

                case MsgType.REQ_SAVE2DB                         : return "сброс данных в БД";

                /*Replies*/
                case MsgType.REP_OK								: return "Успешное завершение запроса";

                case MsgType.REP_BAD_UNKNOWN_REQ_TYPE			: return "Неизвестный тип запроса";
                case MsgType.REP_BAD_USER_NOT_FOUND				: return "Пользователь не найден";
                case MsgType.REP_BAD_USER_NOT_LOGGED_ON			: return "Пользователь не зарегистрирован в системе";
                case MsgType.REP_BAD_USER_SUSPENDED				: return "Пользователь заблокирован";
                case MsgType.REP_BAD_PARTICIPANT_SUSPENDED		: return "Участник заблокирован";
                case MsgType.REP_BAD_USER_ALREADY_LOGGED_ON		: return "Пользователь уже зарегистрирован в системе";
                case MsgType.REP_BAD_INVALID_PASSWORD			: return "Неправильный пароль";
                case MsgType.REP_BAD_BOARD_NOT_FOUND				: return "Режим торгов не найден";
                case MsgType.REP_BAD_SECBOARD_OVERFLOW			: return "Таблица серий срочных контрактов переполнена";
                case MsgType.REP_BAD_SECURITY_NOT_FOUND			: return "Серия срочного контракта не найдена";
                case MsgType.REP_BAD_SECBOARD_EXISTS				: return "Серия срочного контракта уже существует";
                case MsgType.REP_BAD_ORDER_OVERFLOW				: return "Таблица заявок переполнена";
                case MsgType.REP_BAD_SECBOARD_NOT_FOUND			: return "Серия срочного контракта не найдена";
                case MsgType.REP_BAD_SECBOARD_NOT_OPEN			: return "Торги по данной серии срочных контрактов не открыты";
                case MsgType.REP_BAD_ACCOUNT_NOT_FOUND			: return "Позиционный регистр не найден";
                case MsgType.REP_BAD_ACCOUNT_NOT_OWNED			: return "Неверный идентификатор позиционного регистра";
	            case MsgType.REP_BAD_ORDER_NOT_FOUND				: return "Заявка не найдена";
                case MsgType.REP_BAD_INVALID_ORDER_STATUS		: return "Неверное состояние заявки";
                case MsgType.REP_BAD_CLIENT_NOT_FOUND			: return "Клиент не найден";
                case MsgType.REP_BAD_CLIENT_NOT_OWNED			: return "Неверный идентификатор клиента";
                case MsgType.REP_BAD_PARTICIPANT_NOT_FOUND		: return "Участник торгов не найден";
                case MsgType.REP_BAD_PARTICIPANT_OVERFLOW		: return "Таблица Участников торгов переполнена";
                case MsgType.REP_BAD_USER_OVERFLOW				: return "Таблица пользователей переполнена";
                case MsgType.REP_BAD_CLIENT_OVERFLOW				: return "Таблица клиентов переполнена";
                case MsgType.REP_BAD_SECTION_OVERFLOW			: return "Таблица секций переполнена";
                case MsgType.REP_BAD_INVALID_SECBOARD_STATUS		: return "Неверное состояние серии срочного контракта";
                case MsgType.REP_BAD_BOARD_OVERFLOW				: return "Таблица режимов торгов переполнена";
                case MsgType.REP_BAD_SECTION_NOT_FOUND			: return "Секция не найдена";
                case MsgType.REP_BAD_SECTYPE_OVERFLOW			: return "Таблица типов срочных контрактов переполнена";
                case MsgType.REP_BAD_SECURITY_OVERFLOW			: return "Таблица срочных контрактов переполнена";
                case MsgType.REP_BAD_SECTYPE_NOT_FOUND			: return "Тип срочных контрактов не найден";
                case MsgType.REP_BAD_TRADE_OVERFLOW				: return "Таблица сделок переполнена";
                case MsgType.REP_BAD_ACCOUNT_OVERFLOW			: return "Таблица позиционных регистров переполнена";
                case MsgType.REP_BAD_CLOWN_NE_ACCOWN				: return "Клиент не является владельцем счета";
                case MsgType.REP_BAD_INMSG_OVERFLOW				: return "Таблица сообщений переполнена";
                case MsgType.REP_BAD_INVALID_PRICE				: return "Ошибка в указании цены";
                case MsgType.REP_BAD_WRONG_PARTICIPANT			: return "Ошибка в идентификаторе участника";
                case MsgType.REP_BAD_CLEARING_NOT_AVAILABLE		: return "Клиринговая система недоступна";

                case MsgType.REP_BAD_CLIENT_SUSPENDED			: return "Участие клиента приостановлено";
                case MsgType.REP_BAD_ACCOUNT_SUSPENDED			: return "Допуск к осуществлению операций с данного позиционного регистра приостановлен";
                case MsgType.REP_BAD_SECTION_SUSPENDED			: return "Работа Секции приостановлена";

                case MsgType.REP_BAD_PARTICIPANT_NOT_SUSPENDED	: return "Допуск Участника торгов не приостановлен";
                case MsgType.REP_BAD_USER_NOT_SUSPENDED			: return "Допуск пользователя к торгам не приостановлен";
                case MsgType.REP_BAD_CLIENT_NOT_SUSPENDED		: return "Участие клиента не приостановлено";
                case MsgType.REP_BAD_ACCOUNT_NOT_SUSPENDED		: return "Допуск к осуществлению операций с данного позиционного регистра не приостановлен";
                case MsgType.REP_BAD_SECTION_NOT_SUSPENDED		: return "Работа Секции не приостановлена";

                case MsgType.REP_BAD_TXTMSG_OVERFLOW				: return "Таблица сообщений переполнена";

                case MsgType.REP_BAD_ACCESS_RIGHTS				: return "Доступ запрещен";
                // Ответы из клиринга
                case MsgType.REP_CLR_LOGON_OK					: return "Регистрация в клиринговой системе завершилось успешно";
                case MsgType.REP_CLR_LOGON_BAD					: return "Регистрация в клиринговой системе была отклонена";
                case MsgType.REP_CLR_ORDER_OK					: return "Заявка может участвововать в мэтчинге";
                case MsgType.REP_CLR_ORDER_BAD					: return "Заявка должна быть отклонена торговой системой";
                case MsgType.REP_CLR_ORDER_CANCEL_OK				: return "Подтверждение снятия заявки в клиринговой системе";
                case MsgType.REP_CLR_ORDER_CANCEL_BAD			: return "Снятие заявки невозможно(***)";
                case MsgType.REP_CLR_TRADE_OK					: return "Подтверждение регистрации сделки в клиринговой системе";
                case MsgType.REP_CLR_TRADE_BAD					: return "Регистрация сделки невозможна (***)";
                case MsgType.REP_CLR_SETTLE_PRICE_OK				: return "Подтверждение регистрации новой расчетной цены";
                case MsgType.REP_CLR_SETTLE_PRICE_BAD			: return "Регистрация расчетной цены невозможна (***)";

                case MsgType.REP_BAD_UNKNOWN_REQUEST				: return "Неизвестный запрос";
                case MsgType.REP_BAD_INVALID_QTY					: return "Неверно указано количество";
                case MsgType.REP_BAD_ACCOUNT_TYPE				: return "Ошибка в спецификации типа счета";
                case MsgType.REP_BAD_NO_BOARD_SECURITIES			: return "Ни одна серия срочных контрактов не представлена для режима торгов";
                case MsgType.REP_BAD_INVALID_SEC_PARAMS			: return "Зарезервировано. Не используется в текущей версии";

                case MsgType.REP_BAD_OPERDAY_CLOSED				: return "Торги закрыты";

                case MsgType.REP_BAD_NO_BOARD_POSITIONS			: return "Ни один из регистров не представлен в данном режиме торгов";
                case MsgType.REP_BAD_ACCOUNT_NOT_ALLOWED			: return "Регистр  не может использоваться в данном режиме торгов";

                case MsgType.REP_CLR_ORDER_REESTR_OK				: return "Подтверждение приема и сверки реестра заявок";
                case MsgType.REP_CLR_ORDER_REESTR_ERROR			: return "Ошибка приема или сверки реестра заявок";
                case MsgType.REP_CLR_TRADE_REESTR_OK				: return "Подтверждение приема и сверки реестра сделок";
                case MsgType.REP_CLR_TRADE_REESTR_ERROR			: return "Ошибка приема или сверки реестра сделок";

                case MsgType.REP_BAD_STATION_NOT_FOUND			: return "Неверный код станции";
                case MsgType.REP_BAD_WRONG_STATION				: return "Неверная станция для указаного направления платежа";

                case MsgType.REP_BAD_NOT_NEGDEALS				: return "Тип заявки не адресная... поле Контрагент заполнено...";

                case MsgType.REP_BAD_NOT_ALL_MANDATORY_FIELDS	: return "Не все обязательные поля заполнены";
                case MsgType.REP_BAD_INVALID_ADDRESSEE			: return "Неверный адресат";
                case MsgType.REP_BAD_CLIENT_ISNOT_ACC_OWNER		: return "Клиент не является владельцем счета";

                case MsgType.REP_BAD_CROSSTRADE_PROHIBITED		: return "Кросс-сделки запрещены";

                case MsgType.REP_BAD_MESSAGE_NOT_CRYPTED			: return "Сообщение не зашифровано";
                case MsgType.REP_BAD_MESSAGE_NOT_SIGNED			: return "Сообщение не подписано";
                case MsgType.REP_BAD_INVALID_SIGNATURE			: return "Ошибка проверки подписи";
                case MsgType.REP_BAD_CERTIFICATE_NOT_FOUND		: return "Сертификат не зарегистрирован на пользователя";

                case MsgType.REP_BAD_ORDER_TYPE_UNDEFINED		: return "Не задан тип заявки";
                case MsgType.REP_BAD_ORDER_PARAMS_UNDEFINED		: return "Не заданы параметры заявки";
                case MsgType.REP_BAD_ORDER_BS_UNDEFINED			: return "Не задано направление заявки";
                case MsgType.REP_BAD_COUNTERPARTY_UNDEFINED		: return "Не задан контрагент";
                case MsgType.REP_BAD_INVALID_MMF					: return "Флаг Маркет-мейкера можно указывать только для лимитированной заявки";
                case MsgType.REP_BAD_ORDER_INVALID_PARAMS		: return "Неверные параметры заявки";

                case MsgType.REP_BAD_ACCOUNT_CANNOT_BUY			: return "Нет прав на использование счета для покупки";
                case MsgType.REP_BAD_ACCOUNT_CANNOT_SELL			: return "Нет прав на использование счета для продажи";
                case MsgType.REP_BAD_INVALID_ORDER_TYPE			: return "Указанный тип заявки неверен для выбранного режима торгов";

                case MsgType.REP_BAD_SECBOARD_CANNOT_BUY			: return "Нет прав для покупки на указанном секборде";
                case MsgType.REP_BAD_SECBOARD_CANNOT_SELL		: return "Нет прав для продажи на указанном секборде";

                case MsgType.REP_BAD_INVALID_BS_FLAG				: return "Неверное направление платежа";

                case MsgType.REP_BAD_TRADESYSTEM_NOT_AVAILABLE	: return "Торговая система недоступна";

                case MsgType.REP_BAD_INVALID_CP_ACCOUNT			: return "Неверный код поз.регистра контрагента";
                case MsgType.REP_BAD_TRN_IS_TOO_BIG				: return "Длина TRN > 32";
                case MsgType.REP_BAD_SEC_IS_INVALID				: return "Инструмент недействителен на текущую дату";

                case MsgType.REP_BAD_EXCANGE_RATE_OVERFLOW		: return "Таблица курсов валют переполнена";
                case MsgType.REP_BAD_CURRENCY_NOT_FOUND			: return "Неверный код валюты";

                case MsgType.REP_TABS_PARAMS						: return "Подтверждение получения параметров таблиц";

                case MsgType.OK_CHANGES							: return "Новые данные по подписке на информационную таблицу";

                case MsgType.REP_BAD_SESSION_SUSPENDED			: return "Торговая	сессия приостановлена";
                case MsgType.REP_BAD_FATAL_ERROR					: return "Внутренняя ошибка";
            }
        }
    }
}
