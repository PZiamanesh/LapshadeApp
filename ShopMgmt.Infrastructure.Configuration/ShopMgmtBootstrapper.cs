using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopMgmt.Application;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;
using ShopMgmt.Infrastructure.EFCore;
using ShopMgmt.Infrastructure.EFCore.Repository;

namespace ShopMgmt.Infrastructure.Configuration;

public class ShopMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<IProductCategoryApplication, ProductCategoryApplication>();
        service.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        service.AddDbContext<LapShadeDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}