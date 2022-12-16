using _LampshadeQuery.Contract.Article;
using _LampshadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class ArticleCategoryModel : PageModel
{
    private readonly IArticleCategoryQuery _articleCategoryQuery;
    public ArticleCategoryQueryModel ArticleCategory { get; set; }

    public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery)
    {
        _articleCategoryQuery = articleCategoryQuery;
    }

    public void OnGet(string id)
    {
        ArticleCategory = _articleCategoryQuery.GetArticleCategory(id);
    }
}
