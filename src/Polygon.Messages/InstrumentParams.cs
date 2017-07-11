using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Параметры инструмента.
    /// </summary>
    [Serializable, ObjectName("INSTR_PARAMS"), PublicAPI]
    [DebuggerDisplay("INSTRPARAMS {Instrument.Code}")]
    public sealed class InstrumentParams : InstrumentMessage
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public InstrumentParams()
        {
            lotSize = 1;
            SessionEndTime = TimeSpan.FromDays(1);
        }

        #region Properties

        /// <summary>
        ///     Лучшая цена на покупку.
        /// </summary>
        public decimal BestBidPrice { get; set; }

        /// <summary>
        ///     Объём по лучшей цене на покупку.
        /// </summary>
        public long BestBidQuantity { get; set; }

        /// <summary>
        ///     Лучшая цена на продажу.
        /// </summary>
        public decimal BestOfferPrice { get; set; }

        /// <summary>
        ///     Объём по лучшей цене на продажу.
        /// </summary>
        public long BestOfferQuantity { get; set; }

        /// <summary>
        ///     Максимально возможная цена.
        /// </summary>
        public decimal TopPriceLimit { get; set; }

        /// <summary>
        ///     Минимально возможная цена.
        /// </summary>
        public decimal BottomPriceLimit { get; set; }

        /// <summary>
        ///     Точность цены.
        /// </summary>
        public uint DecimalPlaces { get; set; }

        /// <summary>
        ///     Цена последней сделки.
        /// </summary>
        public decimal LastPrice { get; set; }

        private long lotSize;
        /// <summary>
        ///     Размер одного лота. Всегда должен быть больше 0.
        /// </summary>
        public long LotSize
        {
            get { return lotSize; }
            set
            {
                if (value > 0)
                {
                    lotSize = value;
                }
            }
        }

        /// <summary>
        ///     Шаг цены инструмента. Например, 0.0025
        /// </summary>
        public decimal PriceStep { get; set; }

        /// <summary>
        ///     Стоимость шага цены инструмента
        /// </summary>
        public decimal? PriceStepValue { get; set; }

        /// <summary>
        ///     Расчётная цена.
        /// </summary>
        public decimal Settlement { get; set; }

        /// <summary>
        ///     Расчётная цена предыдущего торгового дня.
        /// </summary>
        public decimal PreviousSettlement { get; set; }

        /// <summary>
        ///     Теоретическая цена.
        /// </summary>
        public decimal TheorPrice { get; set; }

        /// <summary>
        ///     Волатильность опциона.
        /// </summary>
        public decimal Vola { get; set; }

        /// <summary>
        ///     Признак того, что волатильность транслируется транспортом, из которого получены параметры инструмента
        /// </summary>
        public bool VolaTranslatedByFeed { get; set; }

        /// <summary>
        ///     Время окончания торговой сессии
        /// </summary>
        public TimeSpan SessionEndTime { get; set; }

        /// <summary>
        ///     Открытый интерес
        /// </summary>
        public int OpenInterest { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Запуск посетителя для обработки сообщений.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя.
        /// </param>
        public override void Accept(IMessageVisitor visitor) => visitor.Visit(this);
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddField(LogFieldNames.BestBidPrice, BestBidPrice);
            fmt.AddField(LogFieldNames.BestBidQuantity, BestBidQuantity);
            fmt.AddField(LogFieldNames.BestOfferPrice, BestOfferPrice);
            fmt.AddField(LogFieldNames.BestOfferQuantity, BestOfferQuantity);
            fmt.AddField(LogFieldNames.TopPriceLimit, TopPriceLimit);
            fmt.AddField(LogFieldNames.BottomPriceLimit, BottomPriceLimit);
            fmt.AddField(LogFieldNames.DecimalPlaces, DecimalPlaces);
            fmt.AddField(LogFieldNames.LastPrice, LastPrice);
            fmt.AddField(LogFieldNames.LotSize, LotSize);
            fmt.AddField(LogFieldNames.PriceStep, PriceStep);
            fmt.AddField(LogFieldNames.PriceStepValue, PriceStepValue);
            fmt.AddField(LogFieldNames.Settlement, Settlement);
            fmt.AddField(LogFieldNames.PreviousSettlement, PreviousSettlement);
            fmt.AddField(LogFieldNames.TheorPrice, TheorPrice);
            fmt.AddField(LogFieldNames.Vola, Vola);
            fmt.AddField(LogFieldNames.VolaTranslatedByFeed, VolaTranslatedByFeed);
            fmt.AddField(LogFieldNames.OpenInterest, OpenInterest);
            fmt.AddField(LogFieldNames.SessionEndTime, SessionEndTime);
            return fmt.ToString();
        }

        /// <summary>
        ///     Создать копию объекта
        /// </summary>
        public InstrumentParams Clone() => (InstrumentParams)MemberwiseClone();

        #endregion
    }
}

