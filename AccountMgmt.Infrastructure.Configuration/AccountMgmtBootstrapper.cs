using AccountMgmt.Application;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Application.Contract.Role;
using AccountMgmt.Domain.AccountAgg;
using AccountMgmt.Domain.RoleAgg;
using AccountMgmt.Infrastructure.EFCore;
using AccountMgmt.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountMgmt.Infrastructure.Configuration;

public class AccountMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<IAccountRepository, AccountRepository>();
        service.AddScoped<IAccountApplication, AccountApplication>();

        service.AddScoped<IRoleRepository, RoleRepository>();
        service.AddScoped<IRoleApplication, RoleApplication>();

        service.AddDbContext<AccountContext>(option => option.UseSqlServer(connectionString));
    }
}