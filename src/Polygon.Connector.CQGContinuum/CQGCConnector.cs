﻿using System;
using System.Collections.Generic;
using System.Text;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Транспорт CQG Continuum
    /// </summary>
    internal sealed class CQGCConnector : IConnector, IConnectionStatusProvider
    {
        private readonly CQGCParameters settings;

        #region Private fields

        /// <summary>
        ///     Логгер
        /// </summary>
        private static readonly ILog _Log = LogManager.GetLogger(typeof(CQGCConnector));

        /// <summary>
        ///     Враппер над WebAPI CQG Continuum
        /// </summary>
        private readonly CQGCAdapter adapter;

        /// <summary>
        ///     Фид
        /// </summary>
        private readonly CQGCFeed feed;

        /// <summary>
        ///     Роутер
        /// </summary>
        private readonly CQGCRouter router;

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        private readonly CQGCInstrumentHistoryProvider historyProvider;

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CQGCConnector(
            CQGCParameters settings)
        {
            this.settings = settings;
            adapter = new CQGCAdapter(settings);
            var instrumentConverter = settings.InstrumentConverter; 
            instrumentConverter.Adapter = adapter;
            var instrumentResolver = new CQGCInstrumentResolver(adapter, instrumentConverter);
            feed = new CQGCFeed(adapter, instrumentResolver);
            router = new CQGCRouter(adapter, instrumentResolver);
            historyProvider = new CQGCInstrumentHistoryProvider(adapter, instrumentResolver);

            adapter.ConnectionStatusChanged += AdapterConnectionStatusChanged;
            adapter.UserMessageReceived += UserMessageReceived;
        }

        #endregion

        #region ITransport

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "CQG Continuum";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => feed;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => router;

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        public IInstrumentHistoryProvider HistoryProvider => historyProvider;
        
        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            _Log.Info().Print("Starting CQGCTransport...");
            adapter.Start();
            router.Start();
            _Log.Info().Print("CQGCTransport started");
        }

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        public void Stop()
        {
            _Log.Info().Print("Stopping CQGCTransport...");
            router.Stop();
            adapter.Stop();
            _Log.Info().Print("CQGCTransport stopped");
        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool IConnector.SupportsOrderModification(string account) => true;

        public void Dispose()
        {
            adapter.Dispose();
        }

        #endregion

        #region IConnectionStatusProvider

        /// <summary>
        ///     Состояние соединения
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; private set; }

        /// <summary>
        ///     Событие изменения состояния соединения
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        #endregion

        #region Private methods

        /// <summary>
        ///     Обработчик события изменения статуса соединения от адаптера
        /// </summary>
        private void AdapterConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            ConnectionStatus = e.ConnectionStatus;
            _Log.Debug().PrintFormat("Connection status: {0}", ConnectionStatus);

            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void UserMessageReceived(AdapterEventArgs<UserMessage> args)
        {
            args.MarkHandled();

            var type = (UserMessage.MessageType)args.Message.message_type;
            switch (type)
            {
                case UserMessage.MessageType.CRITICAL_ERROR:
                    _Log.Error().PrintFormat("CQG Error Adapter error\n{0}", FormatUserMessage(args.Message).Preformatted());
                    adapter.Terminate();
                    adapter.Start();
                    break;
                case UserMessage.MessageType.WARNING:
                    _Log.Warn().Print(FormatUserMessage(args.Message).Preformatted());
                    break;
                case UserMessage.MessageType.INFO:
                    _Log.Info().Print(FormatUserMessage(args.Message).Preformatted());
                    break;
                case UserMessage.MessageType.LOG:
                    _Log.Debug().Print(FormatUserMessage(args.Message).Preformatted());
                    break;
            }
        }

        private static string FormatUserMessage(UserMessage message)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(message.subject))
            {
                builder.Append('[').Append(message.subject).Append("] ");
            }
            if (!string.IsNullOrEmpty(message.source))
            {
                builder.Append(message.source).Append(": ");
            }
            builder.Append(message.text);
            return builder.ToString();
        }

        #endregion
    }
}

