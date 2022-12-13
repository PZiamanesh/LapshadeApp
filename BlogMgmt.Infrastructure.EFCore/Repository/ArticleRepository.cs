using _Framework.Application;
using _Framework.Infrastructure;
using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public EditArticle GetDetails(long id)
    {
        return _context.Articles
            .Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PublishDate = x.PublishDate.ToString(),
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress ?? "",
                CategoryId = x.CategoryId
            }).FirstOrDefault(x => x.Id == id);
    }

    public Article GetWithDescendants(long id)
    {
        return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = _context.Articles
            .Include(x => x.Category)
            .Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                Category = x.Category.Name,
                CategoryId = x.CategoryId
            });

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
        {
            query = query.Where(x => x.Title == searchModel.Title);
        }

        if (searchModel.CategoryId > 0)
        {
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        }

        return query.AsNoTracking().ToList();
    }
}
