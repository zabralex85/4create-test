using FileManager.Domain.Entities;

namespace FileManager.Application.Files.Queries.GetFilesWithFiltersQuery;

public class FileDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<FileEntity, FileDto>();
        }
    }
}
