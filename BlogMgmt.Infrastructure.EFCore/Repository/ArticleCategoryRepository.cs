using _Framework.Application;
using _Framework.Infrastructure;
using BlogMgmt.Application.Contract.ArticleCategory;
using BlogMgmt.Domain.ArticleCategoryAgg;

namespace BlogMgmt.Infrastructure.EFCore.Repository;

public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
{
    private readonly BlogContext _context;

    public ArticleCategoryRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public EditArticleCategory GetDetails(long id)
    {
        return _context.ArticleCategories
            .Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress
            })
            .FirstOrDefault(x => x.Id == id);

    }

    public string GetSlug(long categoryId)
    {
        return _context.ArticleCategories.FirstOrDefault(x=>x.Id == categoryId).Slug;
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            Description = x.Description,
            ShowOrder = x.ShowOrder,
            CreationDate = x.CreationDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name == searchModel.Name);
        }

        return query.ToList();
    }
}
