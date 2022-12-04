using _Framework.Domain;
using BlogMgmt.Application.Contract.ArticleCategory;

namespace BlogMgmt.Domain.ArticleCategoryAgg;

public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
{
    ArticleCategoryViewModel GetDetails(long id);
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
}
