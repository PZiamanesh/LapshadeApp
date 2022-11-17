﻿using DiscountMgmt.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountMgmt.Infrastructure.EFCore.Mapping;

public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
{
    public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
    {
        builder.ToTable("CustomerDiscounts");
        builder.HasKey(x => x.Id);
    }
}
