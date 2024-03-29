﻿using AccountMgmt.Domain.AccountAgg;
using AccountMgmt.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountMgmt.Infrastructure.EFCore;

public class AccountContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
