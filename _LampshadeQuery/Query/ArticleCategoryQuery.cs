using _LampshadeQuery.Contract.ArticleCategory;
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
}

