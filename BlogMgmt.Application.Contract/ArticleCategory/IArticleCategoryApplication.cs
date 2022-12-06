using _Framework.Application;

namespace BlogMgmt.Application.Contract.ArticleCategory;

public interface IArticleCategoryApplication
{
    Task<OperationResult> Create(CreateArticleCategory command);
    Task<OperationResult> Edit(EditArticleCategory command);
    EditArticleCategory GetDetails(long id);
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
}