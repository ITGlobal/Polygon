#region

using System;
using System.Text;

#endregion

namespace Polygon.Connector.MicexBridge.MTETypes
{
    public struct Field
    {
        public string DefaultValue;
        public string Description;
        public EnumType? EnumType;
        public FieldFlags Flags;
        public string Name;
        public int Size;
        public FieldType Type;


        public static string GenerateFields(Field[] inputFields, int decimals, params object[] param)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < inputFields.Length; i++)
            {
                switch (inputFields[i].Type)
                {
                    case FieldType.Char:
                        sb.AppendFormat(string.Concat("{0,-", inputFields[i].Size, "}"),i < param.Length ? param[i] : string.Empty);
                        break;
                    case FieldType.Integer:
                        object obj = param[i];
                        long iPar = obj is int ? (int) obj : (long) obj;

                        sb.Append((iPar).ToString("D" + inputFields[i].Size));
                        break;
                    case FieldType.Fixed:
                        sb.Append(((decimal) param[i]*100m).ToString("D" + inputFields[i].Size));
                        break;
                    case FieldType.Float:
                        decimal dPar = (decimal) param[i];
                        sb.Append(((int)(dPar * decimals)).ToString("D" + (inputFields[i].Size - (dPar < 0 ? 1 : 0))));
                        break;
                    case FieldType.Date:
                        sb.Append(((DateTime) param[i]).ToString("yyyyMMdd"));
                        break;
                    case FieldType.Time:
                        if (param[i] is DateTime)
                            sb.Append(((DateTime) param[i]).ToString("HHmmss"));
                        else
                            sb.Append(param[i].ToString());
                        break;
                }
            }

            return sb.ToString();
        }
    }

    public enum FieldType
    {
        Char = 0,
        Integer = 1,
        Fixed = 2,
        Float = 3,
        Date = 4,
        Time = 5,
    }

    [Flags]
    public enum FieldFlags
    {
        Key = 1,
        SecCode = 2
    }
}