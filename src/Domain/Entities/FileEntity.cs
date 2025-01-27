namespace FileManager.Domain.Entities;

public class FileEntity : BaseAuditableEntity
{
    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = "application/json";

    public string JsonContent { get; set; } = string.Empty;

    public DateTime UploadedDate { get; set; }

    public void MarkUploaded()
    {
        AddDomainEvent(new FileCreatedEvent(this));
    }
}
