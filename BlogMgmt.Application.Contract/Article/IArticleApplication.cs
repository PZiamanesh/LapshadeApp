using _Framework.Application;

namespace BlogMgmt.Application.Contract.Article;

public interface IArticleApplication
{
    Task<OperationResult> Create(CreateArticle command);
    Task<OperationResult> Edit(EditArticle command);
    EditArticle GetDetails(long id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);
}
