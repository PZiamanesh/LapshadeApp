using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopMgmt.Domain.ProductPictureAggr;

namespace ShopMgmt.Infrastructure.EFCore.Mapping;
#nullable disable

public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
{
    public void Configure(EntityTypeBuilder<ProductPicture> builder)
    {
        builder.ToTable("ProductPictures");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Picture).IsRequired();
        builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired();
        builder.Property(x => x.PictureAlt).HasMaxLength(500).IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Pictures)
            .HasForeignKey(x => x.ProductId);
    }
}
