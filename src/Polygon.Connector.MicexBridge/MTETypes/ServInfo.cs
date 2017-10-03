#region

using System;
using System.Text;

#endregion

namespace Polygon.Connector.MicexBridge.MTETypes
{
    public struct ServInfo
    {
        public byte Beta_version;
        public string BoardsSelected;
        public int Connected_To_Micex;
        public byte Debug_flag;
        public DateTime Event_Date;
        public string MICEX_Sever_Name;
        public int Next_Event;
        public string ServerIp;
        public int Session_Id;
        public int Start_Time;
        public int Stop_Time_Max;
        public int Stop_Time_Min;
        public char SystemId;
        public byte Test_flag;
        public string UserID;
        public byte Version_Build;
        public byte Version_Major;
        public byte Version_Minor;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Beta_version = {0}\n", Beta_version);
            sb.AppendFormat("BoardsSelected = {0}\n", BoardsSelected);
            sb.AppendFormat("Connected_To_Micex = {0}\n", Connected_To_Micex);
            sb.AppendFormat("Debug_flag = {0}\n", Debug_flag);
            sb.AppendFormat("Event_Date = {0}\n", Event_Date);
            sb.AppendFormat("MICEX_Sever_Name = {0}\n", MICEX_Sever_Name);
            sb.AppendFormat("Next_Event = {0}\n", Next_Event);
            sb.AppendFormat("ServerIp = {0}\n", ServerIp);
            sb.AppendFormat("Session_Id = {0}\n", Session_Id);
            sb.AppendFormat("Start_Time = {0}\n", Start_Time);
            sb.AppendFormat("Stop_Time_Max = {0}\n", Stop_Time_Max);
            sb.AppendFormat("Stop_Time_Min = {0}\n", Stop_Time_Min);
            sb.AppendFormat("SystemId = {0}\n", SystemId);
            sb.AppendFormat("Test_flag = {0}\n", Test_flag);
            sb.AppendFormat("UserID = {0}\n", UserID);
            sb.AppendFormat("Version_Build = {0}\n", Version_Build);
            sb.AppendFormat("Version_Major = {0}\n", Version_Major);
            sb.AppendFormat("Version_Minor = {0}\n", Version_Minor);

            return sb.ToString();
        }
    }
}