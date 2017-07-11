using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Заявка на бирже.
    /// </summary>
    [Serializable, ObjectName("ORDER"), DebuggerDisplay("{ToString()}"), PublicAPI]
    public sealed class Order : INotifyPropertyChanged, IPrintable
    {
        #region Fields

        private readonly object syncRoot = new object();

        private string account;
        private string clientCode;
        private uint activeQuantity;
        private Instrument instrument;
        private OrderOperation operation;
        private string orderExchangeId;
        private Guid transactionId;
        private decimal price;
        private OrderType type;
        private uint quantity;
        private OrderState state;
        private DateTime dateTime;
        private string comment;

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор по умолчанию.
        /// </summary>
        public Order() { }

        /// <summary>
        ///     Конструктор из транзакции
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция
        /// </param>
        public Order(NewOrderTransaction transaction)
        {
            Account = transaction.Account;
            Instrument = transaction.Instrument;
            TransactionId = transaction.TransactionId;

            ActiveQuantity = transaction.Quantity;
            ClientCode = transaction.ClientCode;
            Operation = transaction.Operation;
            Price = transaction.Price;
            Type = transaction.Type;
            Quantity = transaction.Quantity;

            State = OrderState.New;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Номер счета заявки.
        /// </summary>
        public string Account
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return account;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == account)
                    {
                        return;
                    }
                    account = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Код клиента.
        /// </summary>
        public string ClientCode
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return clientCode;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == clientCode)
                    {
                        return;
                    }
                    clientCode = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Активное (пока не исполнившееся) количество.
        /// </summary>
        public uint ActiveQuantity
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return activeQuantity;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == activeQuantity)
                    {
                        return;
                    }
                    activeQuantity = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Информация об инструменте.
        /// </summary>
        public Instrument Instrument
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return instrument;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (Equals(value, instrument))
                    {
                        return;
                    }
                    instrument = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Операция заявки.
        /// </summary>
        public OrderOperation Operation
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return operation;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == operation)
                    {
                        return;
                    }
                    operation = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public string OrderExchangeId
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return orderExchangeId;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == orderExchangeId)
                    {
                        return;
                    }
                    orderExchangeId = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Идентификатор транзакции.
        /// </summary>
        public Guid TransactionId
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return transactionId;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value.Equals(transactionId))
                    {
                        return;
                    }
                    transactionId = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Цена заявки.
        /// </summary>
        public decimal Price
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return price;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == price)
                    {
                        return;
                    }
                    price = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Тип заявки: Limit/Market
        /// </summary>
        public OrderType Type
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return type;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == type)
                    {
                        return;
                    }
                    type = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Количество контрактов/лотов/акций.
        /// </summary>
        public uint Quantity
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return quantity;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == quantity)
                    {
                        return;
                    }
                    quantity = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Состояние заявки.
        /// </summary>
        public OrderState State
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return state;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == state)
                    {
                        return;
                    }
                    state = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Дата и время заявки
        /// </summary>
        public DateTime DateTime
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return dateTime;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value.Equals(dateTime))
                    {
                        return;
                    }
                    dateTime = value;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Комментарий заявки
        /// </summary>
        public string Comment
        {
            [DebuggerStepThrough]
            get
            {
                lock (syncRoot)
                {
                    return comment;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    if (value == comment)
                    {
                        return;
                    }
                    comment = value;
                }

                OnPropertyChanged();
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Обработать сообщение <see cref="OrderStateChangeMessage"/>
        /// </summary>
        /// <param name="message">
        ///     Сообщение для обработки
        /// </param>
        public OrderChange AcceptStateMessage(OrderStateChangeMessage message)
        {
            lock (syncRoot)
            {
                var change = OrderChange.NotChanged;
                change.OrderExchangeId = OrderExchangeId;
                change.Quantity = Quantity;
                change.ActiveQuantity = ActiveQuantity;

                if (!string.IsNullOrEmpty(message.OrderExchangeId) &&
                    OrderExchangeId != message.OrderExchangeId)
                {
                    OrderExchangeId = message.OrderExchangeId;
                    change.OrderExchangeId = OrderExchangeId;
                }

                if (message.State != null && State != message.State)
                {
                    State = message.State.Value;
                    change.State = State;
                }

                if (message.Price.HasValue &&
                    Price != message.Price.Value)
                {
                    Price = message.Price.Value;
                    change.Price = Price;
                }

                if (message.Quantity.HasValue &&
                    Quantity != message.Quantity.Value)
                {
                    Quantity = message.Quantity.Value;
                    change.Quantity = Quantity;
                }

                if (message.ActiveQuantity != null && ActiveQuantity != message.ActiveQuantity)
                {
                    ActiveQuantity = message.ActiveQuantity.Value;
                    change.ActiveQuantity = ActiveQuantity;
                }

                change.FilledQuantity = message.FilledQuantity;

                return change;
            }
        }

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.ClientCode, ClientCode);
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddField(LogFieldNames.ExchangeOrderId, OrderExchangeId);
            fmt.AddField(LogFieldNames.TransactionId, TransactionId);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddField(LogFieldNames.ActiveQuantity, ActiveQuantity);
            fmt.AddEnumField(LogFieldNames.State, State);
            fmt.AddField(LogFieldNames.Time, DateTime);
            fmt.AddField(LogFieldNames.Comment, Comment);
            fmt.AddEnumField(LogFieldNames.Type, Type);
            return fmt.ToString();
        }

        #endregion
    }
}

