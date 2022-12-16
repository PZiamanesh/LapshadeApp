using _LampshadeQuery.Contract.Article;
using _LampshadeQuery.Contract.ArticleCategory;
using _LampshadeQuery.Query;
using BlogMgmt.Application;
using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Application.Contract.ArticleCategory;
using BlogMgmt.Domain.ArticleAgg;
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

        service.AddScoped<IArticleRepository, ArticleRepository>();
        service.AddScoped<IArticleApplication, ArticleApplication>();

        service.AddScoped<IArticleQuery, ArticleQuery>();
        service.AddScoped<IArticleCategoryQuery, ArticleCategoryQuery>();

        service.AddDbContext<BlogContext>(option => option.UseSqlServer(connectionString));
    }
}