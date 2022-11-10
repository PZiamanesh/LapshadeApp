using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Domain.ProductCategoryAgg;
using ShopMgmt.Infrastructure.EFCore.Mapping;

namespace ShopMgmt.Infrastructure.EFCore;

public class LampShadeDbContext:DbContext
{
    public DbSet<ProductCategory>? ProductCategories { get; set; }
    public DbSet<Product>? Products { get; set; }

    public LampShadeDbContext(DbContextOptions<LampShadeDbContext> options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var efCoreAssembly = typeof(ProductCategoryMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(efCoreAssembly);
        base.OnModelCreating(modelBuilder);
    }
}