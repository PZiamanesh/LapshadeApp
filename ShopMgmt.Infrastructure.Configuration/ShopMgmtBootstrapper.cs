using _Framework.Application;
using _Framework.Infrastructure;
using _LampshadeQuery.Contract.Product;
using _LampshadeQuery.Contract.ProductCategory;
using _LampshadeQuery.Contract.Slide;
using _LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopMgmt.Application;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Application.Contract.ProductPicture;
using ShopMgmt.Application.Contract.Slide;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Domain.ProductCategoryAgg;
using ShopMgmt.Domain.ProductPictureAggr;
using ShopMgmt.Domain.SlideAgg;
using ShopMgmt.Infrastructure.EFCore;
using ShopMgmt.Infrastructure.EFCore.Repository;

namespace ShopMgmt.Infrastructure.Configuration;

public class ShopMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<IProductCategoryApplication, ProductCategoryApplication>();
        service.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        service.AddScoped<IProductApplication, ProductApplication>();
        service.AddScoped<IProductRepository, ProductRepository>();

        service.AddScoped<IProductPictureApplication, ProductPictureApplication>();
        service.AddScoped<IProductPictureRepository, ProductPictureRepository>();

        service.AddScoped<ISlideApplication, SlideApplication>();
        service.AddScoped<ISlideRepository, SlideRepository>();

        service.AddScoped<ISlideQuery, SlideQuery>();
        service.AddScoped<IProductCategoryQuery, ProductCategoryQuery>();
        service.AddScoped<IProductQuery, ProductQuery>();

        service.AddDbContext<ShopContext>(opt => opt.UseSqlServer(connectionString));
    }
}