using FileManager.Application.Common.Interfaces;
using FileManager.Application.Common.Security;

namespace FileManager.Application.Files.Queries.GetFileDetails;

[Authorize]
public record GetFileDetailsQuery : IRequest<FileDetailsDto>
{
    public int Id { get; set; }
}

public class GetFileDetailsQueryHandler : IRequestHandler<GetFileDetailsQuery, FileDetailsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FileDetailsDto?> Handle(GetFileDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Files
            .Where(x => x.Id == request.Id)
            .ProjectTo<FileDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
