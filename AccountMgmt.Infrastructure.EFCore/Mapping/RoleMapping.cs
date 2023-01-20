using AccountMgmt.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountMgmt.Infrastructure.EFCore.Mapping;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.HasMany(x => x.Accounts).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);

        builder.OwnsMany(x => x.Permissions, ownedBulder =>
        {
            ownedBulder.ToTable("Permissions");
            ownedBulder.HasKey(x => x.Id);

            ownedBulder.WithOwner(x => x.Role).HasForeignKey(x => x.RoleId);
        });
    }
}
