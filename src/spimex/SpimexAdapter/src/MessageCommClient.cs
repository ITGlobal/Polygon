using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProtoBuf;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    public abstract class MessageCommClient : BinaryCommClient
    {
        #region Fields

        private readonly string username;
        private readonly string password;
        private readonly string info;

        private static int reqidCounter = default(int) + 1;

        private readonly object sync = new object();
        private readonly Dictionary<ulong, TaskCompletionSource<FTEMessage>> requests = new Dictionary<ulong, TaskCompletionSource<FTEMessage>>();
        
        #endregion

        #region ctor

        protected MessageCommClient(ICommClientSettings settings) 
            : base(settings.IniFile, settings.CommonSectonName, settings.ConnSectonName, settings.Crypto)
        {
            username = settings.Username;
            password = settings.Password;
            info = settings.LogonInfo;
        }

        #endregion

        #region Public methods

        public async Task Login()
        {
            Logon logon = new Logon
            {
                uid = username,
                password = password,
                //decimal_scale = 2,
                info = info
            };

            await SendRequestAsync(new FTEMessage(MsgType.REQ_LOGON, logon));
        }

        public void Logoff()
        {
            SendMessage(new FTEMessage(MsgType.REQ_LOGOFF));
        }

        #endregion


        #region Protected methods

        protected override void OnEnvelope(byte[] envelope)
        {
            var env = MessageParser.Deserialize<Envelope>(envelope);
            var count = env.type.Count;

            if (env.reqid != default(ulong))
            {
                TaskCompletionSource<FTEMessage> tcs;

                lock (sync)
                {
                    if (requests.TryGetValue(env.reqid, out tcs))
                    {
                        requests.Remove(env.reqid);
                    }
                }

                if (tcs != null && count == 1)
                {
                    tcs.SetResult(new FTEMessage(env.type.First(), env.data.FirstOrDefault()));
                }
                else
                {
                    //TODO
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    OnMessage(new FTEMessage(env.type[i], env.data.Count > i ? env.data[i] : null));
                }
            }
        }

        protected virtual void OnMessage(FTEMessage messages) { }

        protected Task<FTEMessage> SendMessageAsync(FTEMessage message)
        {
            var reqid = (ulong)Interlocked.Increment(ref reqidCounter);
            var tcs = new TaskCompletionSource<FTEMessage>();
            lock (sync)
            {
                requests[reqid] = tcs;
            }
            this.SendMessage(message, reqid);

            return tcs.Task;
        }

        protected async Task SendRequestAsync(FTEMessage message)
        {
            var reply = await this.SendMessageAsync(message);

            if (!reply.IsReplyOk)
            {
                throw new FTEException(reply.Type);
            }
        }

        protected async Task<T> SendRequestAsync<T>(FTEMessage message)
            where T : class, IExtensible
        {
            var reply = await this.SendMessageAsync(message);

            if (!reply.IsReplyOk)
            {
                throw new FTEException(reply.Type);
            }

            return reply.GetMessage<T>();
        }

        #endregion
        
        #region Private methods

        private void SendMessage(FTEMessage message, ulong reqid = default(ulong))
        {
            Envelope env = new Envelope
            {
                ver = 1,
                reqid = reqid,
                uid = username,
                //cryptoresult = SignCheckResult.SCHECK_OK,
            };

            env.type.Add(message.Type);
            if (message.HasData)
            {
                env.data.Add(message.Body);
            }

            base.SendMessage(env.Serialize());
        }
        
        #endregion
    }
}

