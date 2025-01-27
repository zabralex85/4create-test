using FileManager.Application.Common.Models;
using FileManager.Application.Files.Commands.UploadFile;
using FileManager.Application.Files.Queries.GetFileDetails;
using FileManager.Application.Files.Queries.GetFilesWithFiltersQuery;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Web.Endpoints;

public class Files : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetFilesWithFilters) // Get files with filters
            .MapGet(GetFileDetails, "{id}") // Get a file by ID
            .MapPost(UploadFile); // Upload a new file
    }

    // Get files with filtering
    public async Task<Ok<PaginatedList<FileDto>>> GetFilesWithFilters(
        ISender sender,
        [AsParameters] GetFilesWithFiltersQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    // Get file details by ID
    public async Task<Results<Ok<FileDetailsDto>, NotFound>> GetFileDetails(ISender sender, int id)
    {
        var result = await sender.Send(new GetFileDetailsQuery { Id = id });
        if (result == null) return TypedResults.NotFound();
        return TypedResults.Ok(result);
    }

    [Consumes("multipart/form-data")]
    public async Task<Results<Created<int>, BadRequest>> UploadFile(ISender sender, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        // Validate file type and size
        if (file.Length == 0 || !file.FileName.EndsWith(".json"))
            return TypedResults.BadRequest();

        using var stream = new StreamReader(file.OpenReadStream());
        var content = await stream.ReadToEndAsync(cancellationToken);

        // Create the command and send it
        var command = new UploadFile
        {
            FileName = file.FileName,
            Content = content
        };

        var id = await sender.Send(command, cancellationToken);

        // Return the created resource
        return TypedResults.Created($"/files/{id}", id);
    }
}
