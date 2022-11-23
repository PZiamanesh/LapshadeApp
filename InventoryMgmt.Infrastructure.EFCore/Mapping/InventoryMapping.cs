using InventoryMgmt.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.EFCore.Mapping;

public class InventoryMapping : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");
        builder.HasKey(x => x.Id);

        builder.OwnsMany(x => x.Operations, OwnedBuilder =>
        {
            OwnedBuilder.ToTable("InventoryOperations");
            OwnedBuilder.HasKey(x => x.Id);

            OwnedBuilder.Property(x => x.Description).HasMaxLength(500);

            OwnedBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
        });
    }
}
