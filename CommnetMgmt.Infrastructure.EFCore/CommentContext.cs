using CommentMgmt.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentMgmt.Infrastructure.EFCore;

public class CommentContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }

    public CommentContext(DbContextOptions<CommentContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
