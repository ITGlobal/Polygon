#region

using System;
using System.Runtime.InteropServices;
using System.Text;
using Polygon.Connector.MicexBridge.MTETypes;

#endregion

namespace Polygon.Connector.MicexBridge
{
    internal class MemoryReader
    {
        private static readonly Encoding encoding = Encoding.GetEncoding(1251);
        private readonly byte[] data;
        private int index;

        public MemoryReader(IntPtr ptr, int length)
        {
            data = new byte[length];

            Marshal.Copy(ptr, data, 0, length);
        }


        public int ReadInt32()
        {
            int r = BitConverter.ToInt32(data, index);
            index += 4;
            return r;
        }

        public byte ReadByte()
        {
            byte r = data[index];
            index++;
            return r;
        }

        public string ReadString(int length)
        {
            string r = encoding.GetString(data, index, length);
            index += length;
            return r.TrimEnd(' ', '\0');
        }

        public string ReadNullString()
        {
            int start = index;

            for (; data[index] != 0; index++) ;

            return encoding.GetString(data, start, index++ - start);
        }

        public string ReadString()
        {
            int length = ReadInt32();

            string r = encoding.GetString(data, index, length);
            index += length;
            return r.TrimEnd(' ', '\0');
        }

        public EnumType ReadEnum()
        {
            EnumType enumType = new EnumType
                                    {
                                        Name = ReadString(),
                                        Description = ReadString(),
                                        Size = ReadInt32(),
                                        Kind = (EnumKind) ReadInt32(),
                                        Constants = new string[ReadInt32()]
                                    };

            for (int j = 0; j < enumType.Constants.Length; j++)
                enumType.Constants[j] = ReadString();

            return enumType;
        }

        public TableType ReadTable(EnumType[] enumTypes)
        {
            return new TableType
                       {
                           Name = ReadString(),
                           Description = ReadString(),
                           Flags = (TableFlags) ReadInt32(),
                           Input = ReadFields(true, enumTypes),
                           Output = ReadFields(false, enumTypes)
                       };
        }

        private Field[] ReadFields(bool isInput, EnumType[] enumTypes)
        {
            Field[] fields = new Field[ReadInt32()];

            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = new Field
                                {
                                    Name = ReadString(),
                                    Description = ReadString(),
                                    Size = ReadInt32(),
                                    Type = (FieldType) ReadInt32(),
                                    Flags = (FieldFlags) ReadInt32()
                                };

                string enumTypeString = ReadString();
                EnumType? enumType = null;
                foreach (EnumType type in enumTypes)
                    if (type.Name == enumTypeString)
                    {
                        enumType = type;
                        break;
                    }
                fields[i].EnumType = enumType;

                fields[i].DefaultValue = isInput ? ReadString() : null;
            }

            return fields;
        }

        public TransactionType ReadTransaction(EnumType[] enumTypes)
        {
            return new TransactionType
                       {
                           Name = ReadString(),
                           Description = ReadString(),
                           Input = ReadFields(true, enumTypes)
                       };
        }

        public MTETable ReadMteTable(Field[] outputFields)
        {
            MTETable mteTable = new MTETable
                                    {
                                        Ref = ReadInt32(),
                                        Rows = ReadRows(outputFields)
                                    };

            return mteTable;
        }

        public MTETable ReadMteTable(TableType[] tablesType)
        {
            MTETable mteTable = new MTETable
                                    {
                                        Ref = ReadInt32()
                                    };

            mteTable.Rows = ReadRows(tablesType[mteTable.Ref].Output);

            return mteTable;
        }

        private MTERow[] ReadRows(Field[] outFields)
        {
            MTERow[] rows = new MTERow[ReadInt32()];

            for (int i = 0; i < rows.Length; i++)
            {
                byte fieldsCount = ReadByte();

                index += 4; //int dataLength = ReadInt32();//это вообще нахрен не нужно 

                MTERow row = new MTERow();

                if (fieldsCount != 0)
                {
                    row.FieldNumbers = new byte[fieldsCount];

                    for (byte j = 0; j < fieldsCount; j++)
                        row.FieldNumbers[j] = ReadByte();
                }
                else
                {
                    row.FieldNumbers = new byte[outFields.Length];

                    for (byte j = 0; j < outFields.Length; j++)
                        row.FieldNumbers[j] = j;
                }

                row.FieldData = new string[row.FieldNumbers.Length];

                for (byte j = 0; j < row.FieldNumbers.Length; j++)
                    row.FieldData[j] = ReadString(outFields[row.FieldNumbers[j]].Size);

                rows[i] = row;
            }

            return rows;
        }
    }
}