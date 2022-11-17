using _Framework.Application;
using DiscountMgmt.Application;
using DiscountMgmt.Application.Contract.CustomerDiscount;
using DiscountMgmt.Domain.CustomerDiscountAgg;
using DiscountMgmt.Infrastructure.EFCore;
using DiscountMgmt.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountMgmt.Infrastructure.Configuration;

public class DiscountMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<ICustomerDiscountRepository, CustomerDiscountRepository>();
        service.AddScoped<ICustomerDiscountApplication, CustomerDiscountApplication>();

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        service.AddDbContext<DiscountContext>(opt => opt.UseSqlServer(connectionString));
    }
}
