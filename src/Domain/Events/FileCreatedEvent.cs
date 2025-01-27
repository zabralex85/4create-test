namespace FileManager.Domain.Events;

public class FileCreatedEvent : BaseEvent
{
    public FileCreatedEvent(FileEntity item)
    {
        Item = item;
    }

    public FileEntity Item { get; }
}
