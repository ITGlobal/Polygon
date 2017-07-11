using System;

namespace SpimexAdapter
{
	public partial class InfoCommClient
    {
		private void ProcessRow(SpimexAdapter.FTE.RowChange row)
		{
			switch (row.tabId)
			{
				case SpimexAdapter.FTE.Table.PARTICIPANTS:
					OnInfoParticipant?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoParticipant>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.USERS:
					OnInfoUser?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoUser>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.CLIENTS:
					OnInfoClient?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoClient>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.ACCOUNTS:
					OnInfoAccount?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoAccount>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.SECTIONS:
					OnInfoSection?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoSection>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.BOARDS:
					OnInfoBoard?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoBoard>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.SECTYPES:
					OnInfoSectype?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoSectype>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.SECURITIES:
					OnInfoSecurity?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoSecurity>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.SECBOARDS:
					OnInfoSecboard?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoSecboard>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.HOLDINGS:
					OnInfoHolding?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoHolding>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.ORDERS:
					OnInfoOrder?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoOrder>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.TRADES:
					OnInfoTrade?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoTrade>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.EVENTS:
					OnInfoEvent?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoEvent>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.STATIONS:
					OnInfoStation?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoStation>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.DELIVERYBASIS:
					OnInfoDeliveryBasis?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoDeliveryBasis>(row.rowData));
					break;

				case SpimexAdapter.FTE.Table.CURRENCIES:
					OnInfoCurrency?.Invoke(MessageParser.Deserialize<SpimexAdapter.FTE.InfoCurrency>(row.rowData));
					break;

				default:
					return;
			}
		}
		
		public event Action<SpimexAdapter.FTE.InfoParticipant> OnInfoParticipant;
		
		public event Action<SpimexAdapter.FTE.InfoUser> OnInfoUser;
		
		public event Action<SpimexAdapter.FTE.InfoClient> OnInfoClient;
		
		public event Action<SpimexAdapter.FTE.InfoAccount> OnInfoAccount;
		
		public event Action<SpimexAdapter.FTE.InfoSection> OnInfoSection;
		
		public event Action<SpimexAdapter.FTE.InfoBoard> OnInfoBoard;
		
		public event Action<SpimexAdapter.FTE.InfoSectype> OnInfoSectype;
		
		public event Action<SpimexAdapter.FTE.InfoSecurity> OnInfoSecurity;
		
		public event Action<SpimexAdapter.FTE.InfoSecboard> OnInfoSecboard;
		
		public event Action<SpimexAdapter.FTE.InfoHolding> OnInfoHolding;
		
		public event Action<SpimexAdapter.FTE.InfoOrder> OnInfoOrder;
		
		public event Action<SpimexAdapter.FTE.InfoTrade> OnInfoTrade;
		
		public event Action<SpimexAdapter.FTE.InfoEvent> OnInfoEvent;
		
		public event Action<SpimexAdapter.FTE.InfoStation> OnInfoStation;
		
		public event Action<SpimexAdapter.FTE.InfoDeliveryBasis> OnInfoDeliveryBasis;
		
		public event Action<SpimexAdapter.FTE.InfoCurrency> OnInfoCurrency;
	}
}