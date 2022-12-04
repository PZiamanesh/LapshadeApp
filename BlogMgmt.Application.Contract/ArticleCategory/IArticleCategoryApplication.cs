using _Framework.Application;

namespace BlogMgmt.Application.Contract.ArticleCategory;

public interface IArticleCategoryApplication
{
    Task<OperationResult> Create(CreateArticle command);
    Task<OperationResult> Edit(EditArticle command);
    ArticleCategoryViewModel GetDetails(long id);
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
}