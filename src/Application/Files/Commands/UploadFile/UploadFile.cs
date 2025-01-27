using FileManager.Application.Common.Interfaces;
using FileManager.Application.Common.Security;
using FileManager.Domain.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace FileManager.Application.Files.Commands.UploadFile;

[Authorize]
public class UploadFile : IRequest<int>
{
    public string FileName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public class UploadFileCommandHandler : IRequestHandler<UploadFile, int>
{
    private readonly IApplicationDbContext _context;
    private readonly string _schema;

    public UploadFileCommandHandler(IApplicationDbContext context)
    {
        _context = context;

        // JSON schema for validation (you can also load this from a file or embedded resource)
        _schema = @"
        {
            '$schema': 'http://json-schema.org/draft-07/schema#',
            'type': 'object',
            'properties': {
                'trialId': { 'type': 'string' },
                'title': { 'type': 'string' },
                'startDate': { 'type': 'string', 'format': 'date' },
                'endDate': { 'type': 'string', 'format': 'date' },
                'participants': { 'type': 'integer', 'minimum': 1 },
                'status': { 'type': 'string', 'enum': ['Not Started', 'Ongoing', 'Completed'] }
            },
            'required': ['trialId', 'title', 'startDate', 'status'],
            'additionalProperties': false
        }";
    }

    public async Task<int> Handle(UploadFile request, CancellationToken cancellationToken)
    {
        // Validate JSON content
        var schema = JSchema.Parse(_schema);
        var json = JObject.Parse(request.Content);

        if (!json.IsValid(schema, out IList<string> errors))
        {
            throw new ValidationException($"Invalid JSON content: {string.Join(", ", errors)}");
        }

        // Create a new file entity
        var file = new FileEntity
        {
            FileName = request.FileName,
            JsonContent = request.Content,
            UploadedDate = DateTime.UtcNow
        };

        // Add to context and save changes
        _context.Files.Add(file);
        await _context.SaveChangesAsync(cancellationToken);

        return file.Id;
    }
}
