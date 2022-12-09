using _Framework.Domain;
using BlogMgmt.Application.Contract.Article;

namespace BlogMgmt.Domain.ArticleAgg;

public interface IArticleRepository : IRepository<long, Article>
{
    EditArticle GetDetails(long id);
    Article GetWithDescendants(long id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);
}
