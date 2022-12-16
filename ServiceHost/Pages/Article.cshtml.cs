using _LampshadeQuery.Contract.Article;
using _LampshadeQuery.Contract.ArticleCategory;
using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Domain.ArticleCategoryAgg;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class ArticleModel : PageModel
{
    private readonly IArticleQuery _articleQuery;
    private readonly IArticleCategoryQuery _articleCategoryQuery;

    public ArticleQueryModel Article { get; set; }
    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    public List<ArticleQueryModel> LatestArticles { get; set; }

    public ArticleModel(IArticleQuery articleQuery,
        IArticleCategoryQuery articleCategoryQuery)
    {
        _articleQuery = articleQuery;
        _articleCategoryQuery = articleCategoryQuery;
    }

    public void OnGet(string id)
    {
        Article = _articleQuery.GetArticleDetails(id);
        ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        LatestArticles = _articleQuery.GetLatestArticles();
    }
}
