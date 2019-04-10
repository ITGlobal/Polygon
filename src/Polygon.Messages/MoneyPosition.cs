using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Информация о лимитах по деньгам по заданному счету.
    /// </summary>
    [Serializable, ObjectName("MONEY"), PublicAPI]
    public sealed class MoneyPosition : AccountMessage, IEnumerable<MoneyPositionProperty>
    {
        #region fields

        private readonly object syncRoot = new object();
        private List<MoneyPositionProperty> properties = new List<MoneyPositionProperty>();

        #endregion

        #region Properties

        /// <summary>
        ///     Параметры
        /// </summary>
        public List<MoneyPositionProperty> Properties
        {
            get
            {
                lock (syncRoot)
                {
                    return properties;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    properties = value;
                }
            }
        }

        /// <summary>
        ///     Индексатор. Позволяет получить параметр по его имени.
        ///     Если параметр не существует, то он создается
        /// </summary>
        /// <param name="name">
        ///     Имя параметра
        /// </param>
        [NotNull]
        public MoneyPositionProperty this[string name]
        {
            get
            {
                lock (syncRoot)
                {
                    if (Properties == null)
                    {
                        Properties = new List<MoneyPositionProperty>();
                    }

                    var property = Properties.FirstOrDefault(_ => _.Name == name);
                    if (property == null)
                    {
                        property = new MoneyPositionProperty { Name = name };
                        Properties.Add(property);
                    }

                    return property;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    this[name].Value = value.Value;
                    this[name].Type = value.Type;
                }
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        ///     Запуск посетителя для обработки сообщений.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя.
        /// </param>
        public override void Accept(IMessageVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        ///     Обновить позицию
        /// </summary>
        public void Update(MoneyPosition update)
        {
            if (update == null)
            {
                return;
            }

            foreach (var row in update.Where(_ => _.HasValue))
            {
                this[row.Name] = row;
            }
        }
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.ClientCode, ClientCode);

            lock (syncRoot)
            {
                foreach (var property in properties)
                {
                    switch (property.Type)
                    {
                        case MoneyPositionPropertyType.Decimal:
                            fmt.AddFieldRequired(property.Name.ToLowerInvariant(), property.AsDecimal());
                            break;
                        case MoneyPositionPropertyType.String:
                            fmt.AddField(property.Name.ToLowerInvariant(), property.AsString());
                            break;
                    }
                }
            }

            return fmt.ToString();
        }

        #endregion

        #region IEnumerable<MoneyPositionProperty>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MoneyPositionProperty> GetEnumerator()
        {
            IEnumerable<MoneyPositionProperty> copy;
            lock (syncRoot)
            {
                copy = properties.ToList();
            }

            return copy.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

