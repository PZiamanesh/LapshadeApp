using _Framework.Application;
using _LampshadeQuery.Contract.Article;
using _LampshadeQuery.Contract.ArticleCategory;
using BlogMgmt.Domain.ArticleAgg;
using BlogMgmt.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _LampshadeQuery.Query;

public class ArticleCategoryQuery : IArticleCategoryQuery
{
    private readonly BlogContext _blogContext;

    public ArticleCategoryQuery(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public List<ArticleCategoryQueryModel> GetArticleCategories()
    {
        return _blogContext.ArticleCategories
            .Include(x => x.Articles)
            .Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ArticleCount = x.Articles.Count(),
            }).AsNoTracking().ToList();
    }

    public ArticleCategoryQueryModel GetArticleCategory(string categorySlug)
    {
        return _blogContext.ArticleCategories
             .Include(x => x.Articles)
             .Select(x => new ArticleCategoryQueryModel
             {
                 Name = x.Name,
                 Slug = x.Slug,
                 Picture = x.Picture,
                 PictureAlt = x.PictureAlt,
                 PictureTitle = x.PictureTitle,
                 MetaDescription = x.MetaDescription,
                 Keywords = x.Keywords,
                 CanonicalAddress = x.CanonicalAddress,
                 Articles = MapArticles(x.Articles)
             }).AsNoTracking().FirstOrDefault(x => x.Slug == categorySlug);
    }

    private static List<ArticleQueryModel> MapArticles(List<Article> articles)
    {
        return articles.Select(x => new ArticleQueryModel
        {
            Title = x.Title,
            Slug = x.Slug,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle= x.PictureTitle,
            ShortDescription = x.ShortDescription,
            PublishDate = x.PublishDate.ToFarsi()
        }).ToList();
    }
}
