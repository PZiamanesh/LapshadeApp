﻿using _Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopMgmt.Application;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductAgg;
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

        service.AddScoped<IProductApplication, ProductApplication>();
        service.AddScoped<IProductRepository, ProductRepository>();

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        service.AddDbContext<LampShadeDbContext>(opt => opt.UseSqlServer(connectionString));
    }
}