using FileManager.Domain.Entities;

namespace FileManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<FileEntity> Files { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
