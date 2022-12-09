using BlogMgmt.Domain.ArticleAgg;
using BlogMgmt.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogMgmt.Infrastructure.EFCore;

public class BlogContext : DbContext
{
    public DbSet<ArticleCategory> ArticleCategories { get; set; }
    public DbSet<Article> Articles { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var assembly = typeof(BlogContext).Assembly;
		modelBuilder.ApplyConfigurationsFromAssembly(assembly);
		base.OnModelCreating(modelBuilder);
	}
}
