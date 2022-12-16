using _LampshadeQuery.Contract.Article;
using _LampshadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class ArticleCategoryModel : PageModel
{
    private readonly IArticleCategoryQuery _articleCategoryQuery;
    private readonly IArticleQuery _articleQuery;

    public ArticleCategoryQueryModel ArticleCategory { get; set; }
    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    public List<ArticleQueryModel> LatestArticles { get; set; }

    public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery,
        IArticleQuery articleQuery)
    {
        _articleCategoryQuery = articleCategoryQuery;
        _articleQuery = articleQuery;
    }

    public void OnGet(string id)
    {
        ArticleCategory = _articleCategoryQuery.GetArticleCategory(id);
        ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        LatestArticles = _articleQuery.GetLatestArticles();
    }
}
