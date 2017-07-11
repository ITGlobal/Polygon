using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using Polygon.Connector.QUIKLua.Adapter;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    /// TCS для задачи получения свечей из квика
    /// </summary>
    internal sealed class HistoryDataRequest : TaskCompletionSource<HistoryData>
    {
        #region Fields

        /// <summary>
        /// Потребитель, которому скармливаются полученные данные. В этом классе он нужен для уведомления о проблемах
        /// </summary>
        private readonly IHistoryDataConsumer consumer;

        /// <summary>
        /// Результат выполнения запроса
        /// </summary>
        private HistoryData data;

        #endregion

        #region Properties

        /// <summary>
        /// GUID сообщения-запроса исторических данных, отправленного в квик
        /// </summary>
        public Guid Id { get; }

        #endregion

        #region .ctor

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">GUID сообщения-запроса, отправленного в квик</param>
        /// <param name="instrument">Инструмент</param>
        /// <param name="begin">Начало запрошенного пероида</param>
        /// <param name="end">Конец запрошенного периода</param>
        /// <param name="span">Размер свечи</param>
        public HistoryDataRequest(Guid id, Instrument instrument, DateTime begin, DateTime end, HistoryProviderSpan span)

        {
            Id = id;
            data = new HistoryData(instrument, begin, end, span);
        }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Обработка ответа квика на запрос исторических данных
        /// </summary>
        /// <param name="response"></param>
        public void ProcessResponse(QLHistoryDataResponse response)
        {
            var points = response.candles
                        .Select(_ => new HistoryDataPoint(_.Time, _.h, _.l, _.o, _.c, 0, 0))
                        .ToList();
            
            foreach (var point in points.Where(_ => _.Point >= data.Begin && _.Point <= data.End))
                data.Points.Add(point);

            QLAdapter.Log.Debug().Print($"{data.Points.Count} candles selected from QLHistoryDataResponse and will be pushed to consumer");

            if (data.Points.Count == 0)
            {
                QLAdapter.Log.Debug().Print("Historical data depth limit has been reached",
                    LogFields.Instrument(data.Instrument),
                    LogFields.Span(data.Span));
                TrySetException(new NoHistoryDataException("Historical data depth limit has been reached"));
                return;
            }

            TrySetResult(data);
        }

        #endregion
    }
}

