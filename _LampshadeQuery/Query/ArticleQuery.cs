using _Framework.Application;
using _LampshadeQuery.Contract.Article;
using BlogMgmt.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _LampshadeQuery.Query;

public class ArticleQuery : IArticleQuery
{
    private readonly BlogContext _blogContext;

    public ArticleQuery(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public ArticleQueryModel GetArticleDetails(string slug)
    {
        return _blogContext.Articles
            .Include(x => x.Category)
            .Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PublishDate = x.PublishDate.ToFarsi(),
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug
            }).AsNoTracking().FirstOrDefault(x=>x.Slug == slug);
    }

    public List<ArticleQueryModel> GetLatestArticles()
    {
        return _blogContext.Articles
            .Include(x => x.Category)
            .Where(x => x.PublishDate <= DateTime.Now)
            .Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                PublishDate = x.PublishDate.ToFarsi(),
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            }).AsNoTracking().ToList();
    }
}

