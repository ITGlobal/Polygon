using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CGateAdapter.Messages.FutInfo;
using CGateAdapter.Messages.OptInfo;
using ITGlobal.DeadlockDetection;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// Логика для мэппинга isin-ов инструментов на их коды. Для шлюза cgate.
    /// </summary>
    internal class CGateInstrumentResolver : IInstrumentConverterContext<InstrumentData>, ISubscriptionTester<InstrumentData>
    {
        private readonly IRwLockObject containerLock = DeadlockMonitor.ReaderWriterLock(typeof(CGateInstrumentResolver), "containerLock");
        
        /// <summary>
        /// Словари для мэппинга разных интерпретаций кода инстурмента
        /// isin - длинный код, типа RTS-6.15
        /// shortIsin - короткий код, например RIM6
        /// isinId - интовый айди инструмента в шлюзе
        /// </summary>
        private readonly Dictionary<int, string> mapIsinIdToShortIsin = new Dictionary<int, string>();
        private readonly Dictionary<string, int> mapShortIsinToIsinId = new Dictionary<string, int>();
        private readonly Dictionary<string, string> mapShortIsinToIsin = new Dictionary<string, string>();
        private readonly List<int> futuresIsinIds = new List<int>();
        private readonly List<int> optionsIsinIds = new List<int>();

        private readonly InstrumentConverter<InstrumentData> instrumentConverter;

        public CGateInstrumentResolver(InstrumentConverter<InstrumentData> instrumentConverter)
        {
            this.instrumentConverter = instrumentConverter;
        }
        
        /// <summary>
        /// Получить данные по инструменту 
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns></returns>
        public InstrumentData GetCGateInstrumentData(Instrument instrument)
        {
            return instrumentConverter.ResolveInstrumentAsync(this, instrument).Result;
        }

        public void Handle(CgmFutSessContents message)
        {
            AddPair(InstrumentType.Futures, message.IsinId, message.ShortIsin, message.Isin);
        }

        public void Handle(CgmOptSessContents message)
        {
            AddPair(InstrumentType.Option, message.IsinId, message.ShortIsin, message.Isin);
        }

        /// <summary>
        /// Получить код инстурмента (short isin) по isinId
        /// </summary>
        public string GetShortIsinByIsinId(int isinId)
        {
            string isin;
            using (containerLock.ReadLock())
            {
                if (!mapIsinIdToShortIsin.TryGetValue(isinId, out isin))
                {
                    return string.Empty;
                }
            }

            return isin;
        }

        /// <summary>
        /// Получить isinId по коду инструмента
        /// </summary>
        public int GetIsinIdByShortIsin(string code)
        {
            int isinId;

            using (containerLock.ReadLock())
            {
                if (!mapShortIsinToIsinId.TryGetValue(code, out isinId))
                {
                    return int.MinValue;
                }
            }

            return isinId;
        }

        /// <summary>
        /// Получить isin инструмента по коду инструмента (shortIsin)
        /// </summary>
        public string GetIsinByShortIsin(string code)
        {
            string isin;

            using (containerLock.ReadLock())
            {
                if (!mapShortIsinToIsin.TryGetValue(code, out isin))
                {
                    return string.Empty;
                }
            }

            return isin;
        }

        /// <summary>
        /// Возвращает тип инструмента по его коду
        /// </summary>
        public InstrumentType GetInstrumentType(string shortIsin)
        {
            var isinId = GetIsinIdByShortIsin(shortIsin);
            if (isinId == int.MinValue)
            {
                return InstrumentType.Undef;
            }

            return GetInstrumentType(isinId);
        }


        /// <summary>
        /// Событие извещает о том, что зарезолвился новый isinId и надобы обработать все pending сообщения
        /// </summary>
        public event EventHandler OnNewIsinResolved;

        private void RaiseOnNewIsinResolved()
        {
            var handler = OnNewIsinResolved;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public InstrumentType GetInstrumentType(int isinId)
        {
            using (containerLock.ReadLock())
            {
                if (futuresIsinIds.Contains(isinId))
                {
                    return InstrumentType.Futures;
                }

                if (optionsIsinIds.Contains(isinId))
                {
                    return InstrumentType.Option;
                }

                return InstrumentType.Undef;
            }
        }

        public InstrumentData GetInstrument(string code)
        {
            var instrument = instrumentConverter.ResolveSymbolAsync(this, code).Result;
            return instrumentConverter.ResolveInstrumentAsync(this, instrument).Result;
        }
        
        private void AddPair(InstrumentType type, int isinId, string shortIsin, string isin)
        {
            using (containerLock.WriteLock())
            {
                if (type == InstrumentType.Futures)
                {
                    futuresIsinIds.Add(isinId);
                }
                else
                {
                    optionsIsinIds.Add(isinId);
                }

                mapIsinIdToShortIsin[isinId] = shortIsin;
                mapShortIsinToIsinId[shortIsin] = isinId;
                mapShortIsinToIsin[shortIsin] = isin;
            }

            RaiseOnNewIsinResolved();
        }

        #region IInstrumentConverterContext

        ISubscriptionTester<InstrumentData> IInstrumentConverterContext<InstrumentData>.SubscriptionTester => this;

        #endregion

        #region ISubscriptionTester

        /// <summary>
        ///     Проверить подписку 
        /// </summary>
        Task<SubscriptionTestResult> ISubscriptionTester<InstrumentData>.TestSubscriptionAsync(InstrumentData data)
        {
            var result = SubscriptionTestResult.Failed();

            using (containerLock.WriteLock())
            {
                if (mapShortIsinToIsin.ContainsKey(data.Symbol))
                {
                    result = SubscriptionTestResult.Passed();
                }
            }
            
            return Task.FromResult(result);
        }

        #endregion
    }
}

