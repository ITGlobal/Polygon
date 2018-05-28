using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    internal sealed class CQGCInstrumentResolver
    {
        private sealed class ResolutionRequest : TaskCompletionSource<uint>
        {
            private readonly uint id;
            private readonly Instrument instrument;
            private readonly Stopwatch timer = new Stopwatch();

            public ResolutionRequest(uint id, Instrument instrument)
            {
                this.id = id;
                this.instrument = instrument;
            }

            public uint Id => id;
            public Instrument Instrument => instrument;

            public void OnSent() => timer.Start();

            public void Resolve(uint value)
            {
                timer.Stop();
                this.TrySetResultBackground(value);

                _Log.Debug().PrintFormat("ResolutionRequest(#{0}, {1}) - resolved as #{2} in {3}ms", id, instrument, value, timer.ElapsedMilliseconds);
            }

            public void Reject()
            {
                timer.Stop();
                this.TrySetCanceledBackground();
                _Log.Debug().PrintFormat("ResolutionRequest(#{0}, {1}) - rejected in {2}ms", id, instrument, timer.ElapsedMilliseconds);
            }
        }

        private static readonly ILog _Log = LogManager.GetLogger<CQGCInstrumentResolver>();
        
        private readonly CQGCAdapter adapter;

        private readonly ILockObject cacheLock = DeadlockMonitor.Cookie<CQGCInstrumentResolver>("cacheLock");
        private readonly Dictionary<Instrument, uint> cachedContractIds = new Dictionary<Instrument, uint>();
        private readonly Dictionary<uint, Instrument> cachedContracts = new Dictionary<uint, Instrument>();
        private readonly Dictionary<uint, decimal> cachedContractPriceScales = new Dictionary<uint, decimal>();

        private readonly ILockObject resolutionRequestsLock = DeadlockMonitor.Cookie<CQGCInstrumentResolver>("resolutionRequestsLock");
        private readonly Dictionary<uint, ResolutionRequest> resolutionRequestsById = new Dictionary<uint, ResolutionRequest>();
        private readonly Dictionary<Instrument, ResolutionRequest> resolutionRequestsByInstrument = new Dictionary<Instrument, ResolutionRequest>();

        private readonly Timer requestBatchTimer;
        private readonly ILockObject requestBatchLock = DeadlockMonitor.Cookie<CQGCInstrumentResolver>("requestBatchLock");
        private readonly List<InformationRequest> requestBatch = new List<InformationRequest>();
        private const int RequestBatchTimerInterval = 250;

        public CQGCInstrumentResolver(
            CQGCAdapter adapter,
            InstrumentConverter<InstrumentData> instrumentConverter)
        {
            this.adapter = adapter;

            this.adapter.MarketDataResolved += MarketDataResolved;
            this.adapter.MarketDataNotResolved += MarketDataNotResolved;
            this.adapter.ContractMetadataReceived += ContractMetadataReceived;

            InstrumentConverter = instrumentConverter;

            requestBatchTimer = new Timer(_ => SendBatchRequest(), null, 0, RequestBatchTimerInterval);
        }

        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }

        /// <summary>
        ///     Получить Contract ID для инструмента
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <returns>
        ///     Contract ID
        /// </returns>
        public async Task<uint> GetContractIdAsync(Instrument instrument)
        {
            using (LogManager.Scope())
            {
                // Ищем ID контракта в кеше
                uint contractId;
                using (cacheLock.Lock())
                {
                    if (cachedContractIds.TryGetValue(instrument, out contractId))
                    {
                        return contractId;
                    }
                }

                // Ищем или создаем запрос инструмента
                ResolutionRequest request;
                var sendRequest = false;
                using (resolutionRequestsLock.Lock())
                {
                    if (!resolutionRequestsByInstrument.TryGetValue(instrument, out request))
                    {
                        var requestId = adapter.GetNextRequestId();
                        request = new ResolutionRequest(requestId, instrument);

                        resolutionRequestsById.Add(requestId, request);
                        resolutionRequestsByInstrument.Add(instrument, request);

                        sendRequest = true;
                    }
                }

                // Отправляем запрос инструмента, если он был создан
                if (sendRequest)
                {
                    await SendResolutionRequestAsync(request);
                }

                // Дожидаемся отработки запроса
                contractId = await request.Task;
                return contractId;
            }
        }

        /// <summary>
        ///     Получить инструмент по его Contract ID
        /// </summary>
        /// <param name="contractId">
        ///     Contract ID
        /// </param>
        /// <returns>
        ///     Инструмент
        /// </returns>
        public Instrument GetInstrument(uint contractId)
        {
            using (cacheLock.Lock())
            {
                Instrument instrument;
                if (cachedContracts.TryGetValue(contractId, out instrument))
                {
                    return instrument;
                }

                return null;
            }
        }

        /// <summary>
        ///     Обновить контракт по его метаданным
        /// </summary>
        /// <param name="metadata">
        ///     Метаданные контракта
        /// </param>
        /// <param name="dependentObjectDescription">
        ///     Строковое описание объекта, который зависит от инструмента
        /// </param>
        public async Task HandleMetadataAsync(ContractMetadata metadata, string dependentObjectDescription = null)
        {
            using (LogManager.Scope())
            {
                _Log.Debug().Print(
                    "Contract metadata received",
                    LogFields.ContractId(metadata.contract_id),
                    LogFields.Symbol(metadata.contract_symbol),
                    LogFields.CorrectPriceScale(metadata.correct_price_scale),
                    LogFields.DisplayPriceScale(metadata.display_price_scale)
                );
                using (cacheLock.Lock())
                {
                    if (cachedContracts.ContainsKey(metadata.contract_id))
                    {
                        _Log.Debug().Print("Already cached", LogFields.ContractId(metadata.contract_id));
                        return;
                    }
                }

                Instrument instrument = await InstrumentConverter.ResolveSymbolAsync(adapter, metadata.contract_symbol, dependentObjectDescription);
                if(instrument == null)
                {
                    _Log.Warn().Print("Instrument not resolved", LogFields.Symbol(metadata.contract_symbol));
                    return;
                }

                using (cacheLock.Lock())
                {
                    _Log.Debug().Print(
                        "Caching instrument",
                        LogFields.ContractId(metadata.contract_id),
                        LogFields.Symbol(metadata.contract_symbol),
                        LogFields.Instrument(instrument)
                        );

                    cachedContractIds[instrument] = metadata.contract_id;
                    cachedContracts[metadata.contract_id] = instrument;
                    cachedContractPriceScales[metadata.contract_id] = (decimal)metadata.correct_price_scale;
                }

                OnInstrumentResolved(metadata.contract_id);
            }
        }

        /// <summary>
        ///     Обновить контракт по его метаданным
        /// </summary>
        /// <param name="metadata">
        ///     Метаданные контракта
        /// </param>
        public void HandleMetadata(ContractMetadata metadata)
        {
            _Log.Debug().Print(
                "Contract metadata received",
                LogFields.ContractId(metadata.contract_id),
                LogFields.Symbol(metadata.contract_symbol),
                LogFields.CorrectPriceScale(metadata.correct_price_scale),
                LogFields.DisplayPriceScale(metadata.display_price_scale)
                );
            using (cacheLock.Lock())
            {
                if (cachedContracts.ContainsKey(metadata.contract_id))
                {
                    _Log.Debug().Print("Already cached", LogFields.ContractId(metadata.contract_id));
                    return;
                }
            }

            var instrument = InstrumentConverter.ResolveSymbolAsync(adapter, metadata.contract_symbol).Result;
            if(instrument == null)
            {
                _Log.Warn().Print("Instrument not resolved", LogFields.Symbol(metadata.contract_symbol));
                return;
            }

            using (cacheLock.Lock())
            {
                _Log.Debug().Print(
                   "Caching instrument",
                   LogFields.ContractId(metadata.contract_id),
                   LogFields.Symbol(metadata.contract_symbol),
                   LogFields.Instrument(instrument)
                   );

                cachedContractIds[instrument] = metadata.contract_id;
                cachedContracts[metadata.contract_id] = instrument;
                cachedContractPriceScales[metadata.contract_id] = (decimal)metadata.correct_price_scale;
            }

            OnInstrumentResolved(metadata.contract_id);
        }

        

        /// <summary>
        ///     Событие резолва инструмента
        /// </summary>
        public event EventHandler<InstrumentResolverEventArgs> InstrumentResolved;

        private void OnInstrumentResolved(uint contractId)
        {
            var handler = InstrumentResolved;
            if (handler != null)
            {
                handler(this, new InstrumentResolverEventArgs(contractId));
            }
        }

        /// <summary>
        ///     Преобразовать цену к формату CQG
        /// </summary>
        /// <param name="contractId">
        ///     Contract ID
        /// </param>
        /// <param name="price">
        ///     Цена
        /// </param>
        /// <returns>
        ///     Цена в нотации CQG либо null, если нет метаданных
        /// </returns>
        public int? ConvertPrice(uint contractId, decimal price)
        {
            using (cacheLock.Lock())
            {
                decimal scale;
                if (!cachedContractPriceScales.TryGetValue(contractId, out scale))
                {
                    return null;
                }

                var correctedPrice = (int)(scale != 0 ? price / scale : price);
                return correctedPrice;
            }
        }

        /// <summary>
        ///     Преобразовать цену из формата CQG
        /// </summary>
        /// <param name="contractId">
        ///     Contract ID
        /// </param>
        /// <param name="price">
        ///     Цена в нотации CQG
        /// </param>
        /// <returns>
        ///      Цена
        /// </returns>
        public decimal ConvertPriceBack(uint contractId, long price)
        {
            using (cacheLock.Lock())
            {
                decimal scale;
                if (!cachedContractPriceScales.TryGetValue(contractId, out scale))
                {
                    _Log.Error().Print(
                        "Can't find price scale for contract",
                        LogFields.ContractId(contractId),
                        LogFields.Instrument(GetInstrument(contractId)?.Code)
                        );
                    return price;
                }

                var correctedPrice = scale != 0 ? price * scale : price;
                return correctedPrice;
            }
        }

        /// <summary>
        ///     Преобразовать цену из формата CQG
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="price">
        ///     Цена в нотации CQG
        /// </param>
        /// <returns>
        ///      Цена
        /// </returns>
        public decimal ConvertPriceBack(Instrument instrument, long price)
        {
            using (cacheLock.Lock())
            {
                uint contractId;
                if (!cachedContractIds.TryGetValue(instrument, out contractId))
                {
                    return price;
                }

                return ConvertPriceBack(contractId, price);
            }
        }

        private async Task SendResolutionRequestAsync(ResolutionRequest request)
        {
            using (LogManager.Scope())
            {
                var instrument = request.Instrument;
                var data = await InstrumentConverter.ResolveInstrumentAsync(adapter, instrument);

                // если символ не зарезолвился, выходим
                if (data == null)
                {
                    request.Resolve(uint.MaxValue);
                    return;
                }

                var message = new InformationRequest
                {
                    symbol_resolution_request = new SymbolResolutionRequest { symbol = data.Symbol },
                    id = request.Id
                };

                request.OnSent();

                using (requestBatchLock.Lock())
                {
                    requestBatch.Add(message);
                }

                _Log.Debug().PrintFormat(
                    "Sending a symbol resolution request {0} for instrument {1} mapped to symbol {2}",
                    request.Id,
                    instrument,
                    data.Symbol
                    );
            }
        }

        private void SendBatchRequest()
        {
            List<InformationRequest> requests;
            using (requestBatchLock.Lock())
            {
                if (requestBatch.Count == 0)
                {
                    return;
                }

                requests = requestBatch.ToList();
                requestBatch.Clear();
            }

            adapter.SendMessage(requests);
        }

        private void MarketDataResolved(AdapterEventArgs<InformationReport> args)
        {
            var contractId = args.Message.symbol_resolution_report.contract_metadata.contract_id;

            ResolutionRequest request;
            using (resolutionRequestsLock.Lock())
            {
                if (!resolutionRequestsById.TryGetValue(args.Message.id, out request))
                {
                    return;
                }

                using (cacheLock.Lock())
                {
                    cachedContractIds[request.Instrument] = contractId;
                    cachedContracts[contractId] = request.Instrument;
                    cachedContractPriceScales[contractId] = (decimal)args.Message.symbol_resolution_report.contract_metadata.correct_price_scale;
                }

                resolutionRequestsById.Remove(request.Id);
                resolutionRequestsByInstrument.Remove(request.Instrument);
            }

            request.Resolve(contractId);
            args.MarkHandled();
            OnInstrumentResolved(contractId);
            _Log.Debug().Print("Instrument is resolved", LogFields.Instrument(request.Instrument), LogFields.ContractId(contractId), LogFields.RequestId(request.Id));
        }

        private void MarketDataNotResolved(AdapterEventArgs<InformationReport> args)
        {
            ResolutionRequest request;
            using (resolutionRequestsLock.Lock())
            {
                if (!resolutionRequestsById.TryGetValue(args.Message.id, out request))
                {
                    return;
                }

                resolutionRequestsById.Remove(request.Id);
                resolutionRequestsByInstrument.Remove(request.Instrument);
            }

            request.Reject();
            args.MarkHandled();

            _Log.Error().Print("Failed to resolve instrument", LogFields.RequestId(request.Id), LogFields.Message(args.Message.text_message));
        }

        private async void ContractMetadataReceived(AdapterEventArgs<ContractMetadata> args)
        {
            using (LogManager.Scope())
            {
                try
                {
                    args.MarkHandled();

                    await Task.Yield();
                    await HandleMetadataAsync(args.Message);
                }
                catch { }
            }
        }
    }
}

