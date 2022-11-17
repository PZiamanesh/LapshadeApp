using DiscountMgmt.Domain.CustomerDiscountAgg;
using DiscountMgmt.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountMgmt.Infrastructure.EFCore;

public class DiscountContext : DbContext
{
    public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(CustomerDiscountMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
