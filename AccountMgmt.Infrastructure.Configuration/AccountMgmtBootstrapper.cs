using AccountMgmt.Application;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Domain.AccountAgg;
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

        service.AddDbContext<AccountContext>(option => option.UseSqlServer(connectionString));
    }
}