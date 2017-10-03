using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Diagnostics;

namespace Polygon.Connector.MicexBridge
{
	/// <summary>
	/// Обертка методов библиотеки mtesrl.dll - API шлюза ММВБ. Создавать экземпляр объекта нужно
	/// через метод GetInstance. Потокобезопасность обращений к экземпляру обеспечивает вызывающая сторона.
	/// </summary>
	public class MtesrlWrapper
	{
		#region DllImports

		[DllImport("mtesrl.dll")]
		private static extern int MTEConnect(string Params, [MarshalAs(UnmanagedType.LPStr)] StringBuilder ErrorMsg);

		[DllImport("mtesrl.dll")]
		private static extern int MTEDisconnect(int ClientIdx);

		[DllImport("mtesrl.dll")]
		private static extern int MTEGetServInfo(int Idx, out IntPtr pointer, out int Len);

		[DllImport("mtesrl.dll")]
		private static extern string MTEGetVersion();

		[DllImport("mtesrl.dll")]
		private static extern int MTEStructure(int Idx, out IntPtr pointer);

		[DllImport("mtesrl.dll")]
		private static extern int MTEExecTrans(int ClientIdx, string TransactionName, string Params,
											   [MarshalAs(UnmanagedType.LPStr)] StringBuilder ResultMsg);

		[DllImport("mtesrl.dll")]
		private static extern int MTEOpenTable(int ClientIdx, string TableName, string Params,
											   bool CompleteFlag, out IntPtr pointer);

		[DllImport("mtesrl.dll")]
		private static extern int MTEAddTable(int ClientIdx, int HTable, int Reference);

		[DllImport("mtesrl.dll")]
		private static extern int MTERefresh(int ClientIdx, out IntPtr pointer);

		[DllImport("mtesrl.dll")]
		private static extern int MTECloseTable(int ClientIdx, int HTable);

		[DllImport("mtesrl.dll")]
		private static extern int MTEFreeBuffer(int ClientIdx); // dummy

		[DllImport("mtesrl.dll")]
		private static extern int MTEGetSnapshot(int Idx, out IntPtr pointer, out int Len);

		[DllImport("mtesrl.dll")]
		private static extern int MTESetSnapshot(int Idx, IntPtr pointer, int Len,
												 [MarshalAs(UnmanagedType.LPStr)] StringBuilder ErrorMsg);

		[DllImport("mtesrl.dll")]
		private static extern string MTEErrorMsg(ErrorCode code);

		#endregion

		private static ILog logger;
		private readonly StringBuilder message = new StringBuilder(256);
		private int clientIdx = -1;

		private static Dictionary<string, MtesrlWrapper> instances = new Dictionary<string, MtesrlWrapper>();
		
		public static MtesrlWrapper GetInstance(string connectionString)
		{
			if (logger == null)
				logger = LogManager.GetLogger(typeof(MtesrlWrapper));

			lock (instances)
			{
				// нормализуем строку удаляя хлам
				connectionString = connectionString.Trim(' ', '\t')
					.Replace('\t', ' ')
					.Replace(" ", "");

				if (!instances.ContainsKey(connectionString))
				{
					var instance = new MtesrlWrapper(connectionString);

					if (instance.clientIdx < (int)ErrorCode.OK)
					{
						instance = null;
						return null;
					}
					else
					{
						instances.Add(connectionString, instance);
						return instance;
					}
				}

				return instances[connectionString];
			}
		}
        
		private MtesrlWrapper(string parameters)
		{
			logger.Info().Print($"Создание экземпляра MtesrlWrapper для подключения {parameters}");

			try
			{
				clientIdx = Connect(parameters);

                //меняем язык сообщений сразу же после соединения
			    string mess;
			    ExecTrans("CHANGE_LANGUAGE", "E", out mess);
			}
			catch (Exception exception)
			{
				logger.Fatal().Print(exception, $"Ошибка при создании MtesrlWrapper для подключения {parameters}.\nОшибка: {exception.Message}");
			}
		}

		public bool IsConnected
		{
			get { return clientIdx >= 0; }
		}

		/// <summary>
		/// Подключение к бриджу.
		/// </summary>
		/// <returns>Идентификатор подключения.</returns>
		private static int Connect(string parameters)
		{
			StringBuilder sb = new StringBuilder(256);
			int code = MTEConnect(parameters, sb);

			if (code < (int) ErrorCode.OK)
				throw new MTEException((ErrorCode) code, sb.ToString());

			return code;
		}

		/// <summary>
		/// ЗАВЕРШЕНИЕ СЕАНСА СВЯЗИ
		/// </summary>
		public void Disconnect()
		{
			ErrorCode code = (ErrorCode) MTEDisconnect(clientIdx);
			clientIdx = -1;
			
			//if (code != ErrorCode.OK)
			//    throw new MTEException(code, MTEErrorMsg(code));
		}

		/// <summary>
		/// ПОЛУЧЕНИЕ ИНФОРМАЦИИ О ШЛЮЗЕ
		/// </summary>
		public ServInfo GetServInfo()
		{
			int len;
			IntPtr pointer;

			ErrorCode code = (ErrorCode) MTEGetServInfo(clientIdx, out pointer, out len);

			if (code != ErrorCode.OK)
				throw new MTEException(code, MTEErrorMsg(code));

			MemoryReader reader = new MemoryReader(pointer, len);

			return new ServInfo
					   {
						   Connected_To_Micex = reader.ReadInt32(),
						   Session_Id = reader.ReadInt32(),
						   MICEX_Sever_Name = reader.ReadString(33),
						   Version_Major = reader.ReadByte(),
						   Version_Minor = reader.ReadByte(),
						   Version_Build = reader.ReadByte(),
						   Beta_version = reader.ReadByte(),
						   Debug_flag = reader.ReadByte(),
						   Test_flag = reader.ReadByte(),
						   Start_Time = reader.ReadInt32(),
						   Stop_Time_Min = reader.ReadInt32(),
						   Stop_Time_Max = reader.ReadInt32(),
						   Next_Event = reader.ReadInt32(),
						   Event_Date = DateTime.ParseExact(reader.ReadInt32().ToString("D8"),
															"ddMMyyyy", MTERow.EnUsCultureInfo),
						   BoardsSelected = reader.ReadNullString(),
						   UserID = reader.ReadString(13),
						   SystemId = (char) reader.ReadByte(),
						   ServerIp = reader.ReadNullString()
					   };
		}

		/// <summary>
		/// ПОЛУЧЕНИЕ ИНФОРМАЦИИ О ВЕРСИИ КЛИЕНТСКОЙ БИБЛИОТЕКИ
		/// </summary>
		public static string GetVersion()
		{
			return MTEGetVersion();
		}

		/// <summary>
		/// ПОЛУЧЕНИЕ ОПИСАНИЯ ИНФОРМАЦИОННЫХ ОБЪЕКТОВ
		/// </summary>
		public void Structure(out TableType[] tablesType, out TransactionType[] transactionsType)
		{
			IntPtr pointer;

			ErrorCode code = (ErrorCode) MTEStructure(clientIdx, out pointer);
			TMTEMsg msg = (TMTEMsg) Marshal.PtrToStructure(pointer, typeof (TMTEMsg));

			MemoryReader reader = new MemoryReader((IntPtr) (pointer.ToInt32() + 4), msg.DataLen);

			if (code != ErrorCode.OK)
				throw new MTEException(code, reader.ReadString(msg.DataLen));


			string interfaceName = reader.ReadString();
			string interfaceDescription = reader.ReadString();

			EnumType[] enumTypes = new EnumType[reader.ReadInt32()];
			for (int i = 0; i < enumTypes.Length; i++)
				enumTypes[i] = reader.ReadEnum();

			int nTables = reader.ReadInt32();
			tablesType = new TableType[nTables];
			for (int i = 0; i < nTables; i++)
			{
				tablesType[i] = reader.ReadTable(enumTypes);
			}

			transactionsType = new TransactionType[reader.ReadInt32()];
			for (int i = 0; i < transactionsType.Length; i++)
				transactionsType[i] = reader.ReadTransaction(enumTypes);
		}

		/// <summary>
		/// Отправка транзакции в шлюз.
		/// </summary>
		/// <param name="TransactionName">Имя транзакции.</param>
		/// <param name="Params">Параметры транзакции в строковом представлении.</param>
		/// <param name="resultMessage">Возвращаемое значение. Сообщение о результате отправки транзакции.</param>
		/// <returns>true если транзакция отправлена успешно, false иначе.</returns>
		public bool ExecTrans(string TransactionName, string Params, out string resultMessage)
		{
			ErrorCode code = (ErrorCode) MTEExecTrans(clientIdx, TransactionName, Params, message);

			resultMessage = message.ToString();

			// если произошла ошибка, сообщаем это возвращая false
			if (code != ErrorCode.OK)
				return false;

			return true;
		}

		/// <summary>
		/// ОТКРЫТИЕ ТАБЛИЦЫ
		/// </summary>
		/// <returns>HTable</returns>
		public int OpenTable(TableType tableType, string Params, bool CompleteFlag, out MTETable mteTable)
		{
			IntPtr pointer;
			int code = MTEOpenTable(clientIdx, tableType.Name, Params, CompleteFlag, out pointer);

			TMTEMsg msg = (TMTEMsg) Marshal.PtrToStructure(pointer, typeof (TMTEMsg));

			MemoryReader reader = new MemoryReader((IntPtr) (pointer.ToInt32() + 4), msg.DataLen);

			if (code < (int) ErrorCode.OK)
				throw new MTEException((ErrorCode) code, reader.ReadString(msg.DataLen));

			mteTable = reader.ReadMteTable(tableType.Output);

			return code;
		}

		/// <summary>
		/// ЗАПРОС ИЗМЕНЕНИЙ
		/// </summary>
		public void AddTable(int HTable, int Reference)
		{
			ErrorCode code = (ErrorCode) MTEAddTable(clientIdx, HTable, Reference);

			if (code != ErrorCode.OK)
				throw new MTEException(code, MTEErrorMsg(code));
		}

		/// <summary>
		/// ПОЛУЧЕНИЕ ИЗМЕНЕНИЙ
		/// </summary>
		public MTETable[] Refresh(TableType[] tablesType)
		{
			IntPtr pointer;
			ErrorCode code = (ErrorCode) MTERefresh(clientIdx, out pointer);

			TMTEMsg msg = (TMTEMsg) Marshal.PtrToStructure(pointer, typeof (TMTEMsg));
			MemoryReader reader = new MemoryReader((IntPtr) (pointer.ToInt32() + 4), msg.DataLen);

			if (code < ErrorCode.OK)
				throw new MTEException(code, reader.ReadString(msg.DataLen));

			MTETable[] mteTables = new MTETable[reader.ReadInt32()];

			for (int i = 0; i < mteTables.Length; i++)
				mteTables[i] = reader.ReadMteTable(tablesType);

			return mteTables;
		}

		/// <summary>
		/// ЗАКРЫТИЕ ТАБЛИЦЫ
		/// </summary>
		public void CloseTable(int HTable)
		{
			ErrorCode code = (ErrorCode) MTECloseTable(clientIdx, HTable);

			if (code != ErrorCode.OK)
				throw new MTEException(code, MTEErrorMsg(code));
		}

		/// <summary>
		/// ОПТИМИЗАЦИЯ ИСПОЛЬЗОВАНИЯ ПАМЯТИ
		/// </summary>
		public void FreeBuffer()
		{
			ErrorCode code = (ErrorCode) MTEFreeBuffer(clientIdx);

			if (code != ErrorCode.OK)
				throw new MTEException(code, MTEErrorMsg(code));
		}

		/// <summary>
		/// ВОССТАНОВЛЕНИЕ ПОСЛЕ СБОЯ НА TESERVER.EXE
		/// </summary>
		public byte[] GetSnapshot()
		{
			int len;
			IntPtr pointer;

			ErrorCode code = (ErrorCode) MTEGetSnapshot(clientIdx, out pointer, out len);

			if (code != ErrorCode.OK)
			{
				if (code == ErrorCode.TSMR)
				{
					byte[] errBuffer = new byte[len];
					Marshal.Copy(pointer, errBuffer, 0, len);
					string msg = (Encoding.GetEncoding(1251)).GetString(errBuffer);

					throw new MTEException(code, msg);
				}
				throw new MTEException(code, MTEErrorMsg(code));
			}

			byte[] snapshot = new byte[len];
			Marshal.Copy(pointer, snapshot, 0, len);

			return snapshot;
		}

		/// <summary>
		/// ВОССТАНОВЛЕНИЕ ПОСЛЕ СБОЯ НА TESERVER.EXE
		/// </summary>
		public void SetSnapshot(byte[] snapshot)
		{
			IntPtr ptr = Marshal.AllocHGlobal(snapshot.Length);

			Marshal.Copy(snapshot, 0, ptr, snapshot.Length);

			int code = MTESetSnapshot(clientIdx, ptr, snapshot.Length, message);

			Marshal.FreeHGlobal(ptr);

			if (code < (int) ErrorCode.OK)
				throw new MTEException((ErrorCode) code, message.ToString());
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct TMTEMsg
	{
		public int DataLen;
	}
}