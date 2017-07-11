using System;
using System.IO;
using CGateAdapter.Messages;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;

namespace CGateAdapter
{
    internal sealed class CGatePublisher : CGateSubconnection
    {
        private readonly ICGateLogger _logger;
        
        private CGateStream _stream;
        private Publisher _publisher;

        /// <summary>
        /// Признак того, что паблишер сконфигурирован правильно
        /// </summary>
        private readonly bool _configuredWell;

        public CGatePublisher(
            string name,
            string schemeFileName,
            string schemeName,
            IMessageConverter converter,
            ICGateStreamCallback callback,
            ICGateLogger logger)
        {
            _logger = logger;

            Name = name;
            SchemeFileName = schemeFileName;
            SchemeName = schemeName;

            if (!File.Exists(schemeFileName))
            {
                _logger.Error($"File {schemeFileName} doesn't exist, publisher {Name} won't be started");
                _configuredWell = false;
                return;
            }

            _stream = new CGateStream(Name, CGateStreamType.FortsMessages, converter, callback, _logger);
            _configuredWell = true;
        }

        public override State State => _publisher.State;

        #region Public methods

        public void Publish(CGateMessage message)
        {
            if (!_configuredWell)
            {
                return;
            }
            
            using (var messageToPost = _publisher.NewMessage(MessageKeyType.KeyName, message.MessageTypeName))
            {
                var dataMessage = (DataMessage)messageToPost;

                message.CopyToDataMessage(dataMessage);

                var extIdObj = dataMessage["ext_id"];
                if (extIdObj != null)
                {
                    dataMessage.UserId = (uint)extIdObj.asInt();
                }
                else
                {
                    dataMessage.UserId = message.UserId;
                }

                _publisher.Post(messageToPost, PublishFlag.NeedReply);
                _logger.Debug($"Publish: UserId={dataMessage.UserId}; Message={message}");
            }
        }

        #endregion

        #region CGateSubconnection

        protected override string ConnectionString => $"p2mq://FORTS_SRV;category=FORTS_MSG;name={Name};timeout=5000;scheme=|FILE|{SchemeFileName}|{SchemeName}";

        public override void Dispose()
        {
            if (!_configuredWell)
                return;

            _disposed = true;

            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            if (_publisher != null)
            {
                _publisher.Dispose();
                _publisher = null;
            }
        }

        public override void SetConnection(Connection connection)
        {
            if (!_configuredWell)
                return;

            _publisher = new Publisher(connection, ConnectionString);
            _stream.SetConnection(connection);
        }

        public override void Open()
        {
            if (!_configuredWell)
                return;

            try
            {
                _publisher.Open();
            }
            catch (CGateException exception)
            {
                _logger.Error(exception, $"{Name}: CGateException: ErrCode={exception.ErrCode}, Message={exception.Message}");
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{Name}: Exception: {exception.Message}");
                throw;
            }

            _logger.Info($"{Name}: Publisher is opened");

            _stream.Open();
        }

        public override void Close()
        {
            if (!_configuredWell)
                return;

            if (!_disposed && _publisher != null && _publisher.State != State.Closed)
            {
                _publisher.Close();
            }

            _stream.Close();
        }

        public override void TryOpen()
        {
            if (!_configuredWell)
                return;

            if (State == State.Closed)
            {
                Open();
            }
            else if (State == State.Error)
            {
                Close();
            }

            _stream.TryOpen();
        }

        #endregion
    }
}
