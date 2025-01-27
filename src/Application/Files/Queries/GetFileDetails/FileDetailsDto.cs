using FileManager.Domain.Entities;

namespace FileManager.Application.Files.Queries.GetFileDetails;

public class FileDetailsDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<FileEntity, FileDetailsDto>();
        }
    }
}
