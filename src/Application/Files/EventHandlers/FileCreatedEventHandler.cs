using FileManager.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FileManager.Application.Files.EventHandlers;

public class FileCreatedEventHandler : INotificationHandler<FileCreatedEvent>
{
    private readonly ILogger<FileCreatedEventHandler> _logger;

    public FileCreatedEventHandler(ILogger<FileCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(FileCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FileManager Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
