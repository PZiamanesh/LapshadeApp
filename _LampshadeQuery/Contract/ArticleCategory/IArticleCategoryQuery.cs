using _LampshadeQuery.Contract.Article;

namespace _LampshadeQuery.Contract.ArticleCategory;

public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
    ArticleCategoryQueryModel GetArticleCategory(string categorySlug);
}
