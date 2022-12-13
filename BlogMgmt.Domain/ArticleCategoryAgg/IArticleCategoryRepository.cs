using _Framework.Domain;
using BlogMgmt.Application.Contract.ArticleCategory;

namespace BlogMgmt.Domain.ArticleCategoryAgg;

public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
{
    EditArticleCategory GetDetails(long id);
    List<ArticleCategoryViewModel> GetCategories();
    string GetSlug(long categoryId);
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
}
