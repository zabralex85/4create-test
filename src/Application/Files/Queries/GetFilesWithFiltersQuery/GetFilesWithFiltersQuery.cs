using FileManager.Application.Common.Interfaces;
using FileManager.Application.Common.Mappings;
using FileManager.Application.Common.Models;
using FileManager.Application.Common.Security;

namespace FileManager.Application.Files.Queries.GetFilesWithFiltersQuery;

[Authorize]
public record GetFilesWithFiltersQuery : IRequest<PaginatedList<FileDto>>
{
    public string CreatedBy { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetFilesWithFiltersQueryHandler : IRequestHandler<GetFilesWithFiltersQuery, PaginatedList<FileDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFilesWithFiltersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<FileDto>> Handle(GetFilesWithFiltersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Files
            .Where(x => x.CreatedBy == request.CreatedBy)
            .OrderBy(x => x.Created)
            .ProjectTo<FileDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
