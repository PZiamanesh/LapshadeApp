using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Infrastructure.EFCore.Mapping;

public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");
        builder.HasKey(x => x.Id);
    }
}