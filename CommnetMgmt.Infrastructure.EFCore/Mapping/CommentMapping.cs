using CommentMgmt.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentMgmt.Infrastructure.EFCore.Mapping;

public class CommentMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(30);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Message).HasMaxLength(1000);

        builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);
    }
}
