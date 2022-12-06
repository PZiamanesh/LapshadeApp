using BlogMgmt.Application;
using BlogMgmt.Application.Contract.ArticleCategory;
using BlogMgmt.Domain.ArticleCategoryAgg;
using BlogMgmt.Infrastructure.EFCore;
using BlogMgmt.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogMgmt.Infrastructure.Configuration;

public class BlogMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
        service.AddScoped<IArticleCategoryApplication, ArticleCategoryApplication>();

        service.AddDbContext<BlogContext>(option => option.UseSqlServer(connectionString));
    }
}