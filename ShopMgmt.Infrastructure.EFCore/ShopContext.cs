using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Domain.ProductCategoryAgg;
using ShopMgmt.Domain.ProductPictureAggr;
using ShopMgmt.Domain.SlideAgg;

namespace ShopMgmt.Infrastructure.EFCore;
#nullable disable

public class ShopContext : DbContext
{
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductPicture> ProductPictures { get; set; }
    public DbSet<Slide> Slides { get; set; }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var efCoreAssembly = typeof(ShopContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(efCoreAssembly);
        base.OnModelCreating(modelBuilder);
    }
}