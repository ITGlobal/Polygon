using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpimexAdapter.Wrapper;

namespace SpimexAdapter
{
    public abstract class BinaryCommClient
    {
        #region Fields

        private readonly string connSectonName;

        private readonly CommModule commClient;
        private IntPtr connect = IntPtr.Zero;

        private readonly CommQueue queueIn = new CommQueue();
        private readonly CommQueue queueOut = new CommQueue();

        private Task outQueueListener;
        private readonly CancellationTokenSource cancelSource = new CancellationTokenSource();
        
        #endregion

        #region ctor

        protected BinaryCommClient(string iniFile, string commonSectonName = null, string connSectonName = null, bool crypto = false)
        {
            this.connSectonName = connSectonName;
            commClient = new CommModule(crypto);

            ThrowIfFail(commClient.Start(iniFile, commonSectonName));
        }

        #endregion

        #region Public methods

        public void Connect()
        {
            ThrowIfFail((connect = commClient.Connect(queueIn, connSectonName)) != IntPtr.Zero);
            commClient.SetQueue(connect, queueOut);

            outQueueListener = Task.Factory.StartNew(_ => ListeningFunc((CancellationToken)_), cancelSource.Token);
        }

        public void Disconnect()
        {
            cancelSource.Cancel();
            outQueueListener?.Wait();

            ThrowIfFail(queueIn.Stop(), queueIn);
            //ThrowIfFail(queueOut.Stop(), queueOut);

            commClient.Disconnect(connect);
            connect = IntPtr.Zero;
            ThrowIfFail(commClient.Stop());

            //queueIn.Dispose();
            //queueOut.Dispose();
        }

        #endregion

        #region Protected methods

        protected void SendMessage(byte[] message)
        {
            ThrowIfFail(queueOut.Put(connect, message, 0), queueOut);
        }

        protected abstract void OnEnvelope(byte[] envelope);

        #endregion

        #region Private methods

        private void ListeningFunc(CancellationToken token)
        {
            Thread.CurrentThread.Name = "SPIMEX_" + GetType().Name.Substring(0, 3).ToUpper();
            while (!token.IsCancellationRequested)
            {
                IntPtr oConnect;
                byte[] message;
                int error;

                if (queueIn.Get(out oConnect, out message, out error, 500))
                {
                    if (error != 0)
                    {
                        // TODO error
                        var errorMsg = CommModule.DefaultEncoding.GetString(message);
                        if (errorMsg.Length > 0 && errorMsg[errorMsg.Length - 1] == '\0')
                        {
                            errorMsg = errorMsg.Substring(0, errorMsg.Length - 1);
                        }
                        OnError?.Invoke(error, errorMsg);
                        //Task.Factory.StartNew(() => OnError?.Invoke(error, errorMsg));
                    }
                    else
                    {
                        // TODO message
                        OnEnvelope(message);
                        //Task.Factory.StartNew(() => OnEnvelope(message));
                    }
                }
                else if (error != 0)
                {
                    // Очередь остановлена
                    return;
                }
            }

            //TODO обработка исключений
        }

        #region Обработка ошибок

        //protected virtual void OnError(int error, string errorMsg) { }
        public event Action<int, string> OnError;

        private void ThrowIfFail(bool result)
        {
            if (!result)
            {
                string msg;
                var error = commClient.GetLastError(connect, out msg);

                throw new Exception(msg);
            }
        }

        private void ThrowIfFail(bool result, CommQueue queue)
        {
            if (!result)
            {
                string msg;
                var error = queue.GetLastError(connect, out msg);

                throw new Exception(msg);
            }
        }

        #endregion

        #endregion
        
    }
}
