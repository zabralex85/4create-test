using FileManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Infrastructure.Data.Configurations;

public class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        // Table name
        builder.ToTable("Files");

        // Primary key
        builder.HasKey(f => f.Id);

        // FileName: Required, max length 255
        builder.Property(f => f.FileName)
            .HasMaxLength(255)
            .IsRequired();

        // ContentType: Required, default value is 'application/json'
        builder.Property(f => f.ContentType)
            .HasMaxLength(50)
            .HasDefaultValue("application/json")
            .IsRequired();

        // JsonContent: Stored as 'jsonb' in PostgreSQL, required
        builder.Property(f => f.JsonContent)
            .HasColumnType("jsonb")
            .IsRequired();

        // UploadedDate: Required
        builder.Property(f => f.UploadedDate)
            .IsRequired();
    }
}
