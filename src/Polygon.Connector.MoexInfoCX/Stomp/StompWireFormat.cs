using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal sealed class StompWireFormat : IWireFormat
    {
        private const char CR = '\r';
        private const char LF = '\n';
        private const string EOL = "\n";
        private const char SEPARATOR = ':';
        private const char TERMINATOR = '\0';

        private static readonly ThreadLocal<StringBuilder> LocalStringBuilder
            = new ThreadLocal<StringBuilder>(() => new StringBuilder());

        private static StringBuilder StringBuilder => LocalStringBuilder.Value;

        public string WriteFrame(IStompFrame frame)
        {
            var sb = StringBuilder;

            // Команда
            sb.Append(frame.Command);
            sb.Append(EOL);

            // Заголовки
            var shouldEncode = ShouldUseHeaderEncoding(frame.Command);
            foreach (var pair in frame.Headers)
            {
                var key = pair.Key;
                foreach (var value in pair.Value)
                {
                    EncodeValue(sb, key, shouldEncode);
                    sb.Append(SEPARATOR);
                    EncodeValue(sb, value, shouldEncode);
                    sb.Append(EOL);
                }
            }
            sb.Append(EOL);

            // Тело
            sb.Append(frame.Body);

            // Терминатор
            sb.Append(TERMINATOR);

            var str = sb.ToString();
            sb.Clear();
            return str;
        }

        public IStompFrame ReadFrame(string rawMessage)
        {
            var msg = new IncomingStompFrame();

            var i = 0;
            ReadCommand(rawMessage, ref i, msg);
            var shouldDecode = ShouldUseHeaderEncoding(msg.Command);
            while (ReadOneHeader(rawMessage, ref i, msg, shouldDecode)) { }
            ReadBody(rawMessage, ref i, msg);

            msg.Validate();

            return msg;
        }
        
        private static void ReadCommand(string rawMessage, ref int i, IncomingStompFrame msg)
        {
            // Читаем строку до символа \r
            var start = i;
            if (!ReadLine(rawMessage, ref i))
            {
                // Команда не найдена
                throw new StompWireFormatException($"Unable to find command (at {i})");
            }

            // Прочитанная строка не должна быть пустой
            var end = i - 1;
            var length = end - start;
            if (length <= 0)
            {
                // Команда пуста
                throw new StompWireFormatException($"Empty command (at {i})");
            }

            var command = rawMessage.Substring(start, length);
            msg.SetCommand(command);
        }

        private static bool ReadOneHeader(string rawMessage, ref int i, IncomingStompFrame msg, bool shouldDecode)
        {
            // Читаем строку до символа \r
            var start = i;
            if (!ReadLine(rawMessage, ref i))
            {
                // Заголовок не найден
                throw new StompWireFormatException($"Unable to find header (at {i})");
            }

            // Если прочитанная строка пуста - это признак завершения заголовков
            var end = i - 1;
            var length = end - start;
            if (length <= 0)
            {
                return false;
            }

            // Внутри диапазона [start; end] ищем первый символ ':'
            var sep = start;
            for (; sep <= end && rawMessage[sep] != SEPARATOR; sep++) { }
            if (rawMessage[sep] != SEPARATOR)
            {
                // Разделитель заголовка не найден
                throw new StompWireFormatException($"Unable to find header separator (at {i})");
            }

            var keyStart = start;
            var keyEnd = sep;
            var keyLength = keyEnd - keyStart;
            if (keyLength <= 0)
            {
                // Ключ заголовка пуст
                throw new StompWireFormatException($"Unable to find header key (at {i})");
            }

            var valueStart = sep + 1;
            var valueEnd = end;
            var valueLength = valueEnd - valueStart;

            var key = DecodeValue(rawMessage, keyStart, keyLength, shouldDecode);
            var value = DecodeValue(rawMessage, valueStart, valueLength, shouldDecode);
            msg.SetHeader(key, value);

            return true;
        }

        private static void ReadBody(string rawMessage, ref int i, IncomingStompFrame msg)
        {
            // Читаем строку до символа \0
            var start = i;
            for (; i < rawMessage.Length && rawMessage[i] != TERMINATOR; i++) { }
            if (i >= rawMessage.Length)
            {
                // Тело не найдено
                return;
            }

            var end = i;
            var length = end - start;
            var body = rawMessage.Substring(start, length);
            msg.SetBody(body);

            // Опционально дочитываем символы \r\n или \n
            while (ReadLine(rawMessage, ref i)) { }
            if (i != rawMessage.Length - 1)
            {
                // После терминатора в сообщении не должно быть ничего, кроме \r\n\
                throw new StompWireFormatException($"Unconsumed data after terminator (at {i})");
            }
        }

        private static bool ReadLine(string rawMessage, ref int i)
        {
            // Читаем строку до символа \r или \n
            var start = i;
            for (; i < rawMessage.Length && rawMessage[i] != CR && rawMessage[i] != LF; i++) { }
            if (i >= rawMessage.Length - 1)
            {
                i = start;
                return false;
            }

            // Если был прочитан символ \r, то после него должен идти \n
            if (rawMessage[i] == CR)
            {
                i++;
                if (i >= rawMessage.Length - 1 || rawMessage[i] != LF)
                {
                    i = start;
                    return false;
                }
            }

            i++;
            return true;
        }

        private static bool ShouldUseHeaderEncoding(string command)
        {
            switch (command)
            {
                case "CONNECT":
                case "CONNECTED":
                    return false;
                default:
                    return true;
            }
        }

        private static void EncodeValue(StringBuilder builder, string value, bool enable = true)
        {
            for (var i = 0; i < value.Length; i++)
            {
                var ch = value[i];
                if (enable)
                {
                    switch (ch)
                    {
                        case '\r':
                            builder.Append(@"\r");
                            break;
                        case '\n':
                            builder.Append(@"\n");
                            break;
                        case ':':
                            builder.Append(@"\c");
                            break;
                        case '\\':
                            builder.Append(@"\\");
                            break;

                        default:
                            builder.Append(ch);
                            break;
                    }
                }
                else
                {
                    builder.Append(ch);
                }
            }
        }

        private static string DecodeValue(string rawMessage, int start, int length, bool enable = true)
        {
            var frame = StringBuilder;

            var end = start + length;
            for (var i = start; i < end; i++)
            {
                var ch = rawMessage[i];
                if (enable && ch == '\\')
                {
                    if (i >= end - 1)
                    {
                        frame.Append(ch);
                    }
                    else
                    {
                        i++;
                        var next = rawMessage[i];
                        switch (next)
                        {
                            case 'r':
                                frame.Append('\r');
                                break;
                            case 'n':
                                frame.Append('\n');
                                break;
                            case 'c':
                                frame.Append(':');
                                break;
                            case '\\':
                                frame.Append('\\');
                                break;
                            default:
                                frame.Append(ch);
                                frame.Append(next);
                                break;
                        }
                    }
                }
                else
                {
                    frame.Append(ch);
                }
            }

            var str = frame.ToString();
            frame.Clear();
            return str;
        }
    }
}