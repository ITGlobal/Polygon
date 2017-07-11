using ru.micexrts.cgate.message;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Интерфейс конвертера cgate сообщений в типизированные сообщения адаптера. Все реализации этого интерфейса - автогенерённые.
    /// </summary>
    internal interface IMessageConverter
    {
        CGateMessage ConvertToAdapterMessage(StreamDataMessage source);
        
        CGateMessage ConvertToAdapterMessage(DataMessage source);
    }
}
