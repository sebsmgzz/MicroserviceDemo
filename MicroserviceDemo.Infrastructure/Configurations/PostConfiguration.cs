namespace MicroserviceDemo.Infrastructure.Configurations;

using MicroserviceDemo.Domain.Models.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ConfigurationConstants
{

    public const string DefaultSchema = "dbo";

}
public class PostConfiguration : IEntityTypeConfiguration<Post>
{

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(70)
            .IsRequired(true);

        builder.Property(p => p.Content)
            .HasColumnName("content")
            .HasMaxLength(280)
            .IsRequired(true);

        builder.Property(p => p.IsActive)
            .HasColumnName("isActive");

        builder.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey("postId")
            .OnDelete(DeleteBehavior.Cascade);

    }

}
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{

    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(c => c.Content)
            .HasColumnName("content")
            .HasMaxLength(280)
            .IsRequired(true);

        builder.Property(c => c.IsActive)
            .HasColumnName("isActive");
    }

}