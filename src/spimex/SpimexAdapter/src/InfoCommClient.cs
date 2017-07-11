using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpimexAdapter.FTE;

namespace SpimexAdapter
{
    public partial class InfoCommClient : MessageCommClient
    {
        private readonly object sync = new object();
        private readonly Dictionary<Table, TaskCompletionSource<bool>> subscriptions = new Dictionary<Table, TaskCompletionSource<bool>>();


        public InfoCommClient(ICommClientSettings settings) 
            : base(settings)
        { }

        private async Task SubscribeTable(Table table)
        {
            var msg = new Subscribe
            {
                tab_id = table,
                max_row_id = -1,
                last_change_id = 0,
            };

            var tcs = new TaskCompletionSource<bool>();
            lock (sync)
            {
                subscriptions[table] = tcs;
            }

            await base.SendRequestAsync(new FTEMessage(MsgType.REQ_SUBSCRIBE, msg));
            await tcs.Task;
        }

        private async Task UnsubscribeTable(Table table)
        {
            var msg = new Unsubscribe
            {
                tab_id = table,
            };

            await base.SendRequestAsync(new FTEMessage(MsgType.REQ_UNSUBSCRIBE, msg));
        }

        private async Task SubscribeTableOnce(Table table)
        {
            await SubscribeTable(table);
            await UnsubscribeTable(table);
        }

        public Task Subscribe(params Table[] tables) => Task.WhenAll(tables.Select(SubscribeTable));

        public Task Unsubscribe(params Table[] tables) => Task.WhenAll(tables.Select(UnsubscribeTable));
        
        public Task SubscribeOnce(params Table[] tables) => Task.WhenAll(tables.Select(SubscribeTableOnce));
        

        protected override void OnMessage(FTEMessage messages)
        {
            switch (messages.Type)
            {
                case MsgType.OK_CHANGES:
                    ProcessChanges(messages);
                    break;

                default:
                    return;
            }
        }

        private void ProcessChanges(FTEMessage message)
        {
            var changesMsg = message.GetMessage<Changes>();

            List<TaskCompletionSource<bool>> tcsList = new List<TaskCompletionSource<bool>>(1);

            foreach (var row in changesMsg.row)
            {
                var rowChange = MessageParser.Deserialize<RowChange>(row);
                ProcessRow(rowChange);

                lock (sync)
                {
                    TaskCompletionSource<bool> tcs;
                    if (subscriptions.TryGetValue(rowChange.tabId, out tcs))
                    {
                        subscriptions.Remove(rowChange.tabId);
                        tcsList.Add(tcs);
                    }
                }
            }

            foreach (var tcs in tcsList)
            {
                tcs.SetResult(true);
            }
        }
    }
}
