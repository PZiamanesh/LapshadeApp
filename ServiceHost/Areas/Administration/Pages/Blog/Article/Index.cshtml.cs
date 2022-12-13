using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article;

public class IndexModel : PageModel
{
    private readonly IArticleCategoryApplication _articleCategoryApplication;
    private readonly IArticleApplication _articleApplication;

    public List<ArticleViewModel>? Articles { get; set; }
    public SelectList? ArticleCategories { get; set; }
    public ArticleSearchModel? SearchModel { get; set; }

    public IndexModel(IArticleCategoryApplication articleCategoryApplication,
        IArticleApplication articleApplication)
    {
        _articleCategoryApplication = articleCategoryApplication;
        _articleApplication = articleApplication;
    }

    public void OnGet(ArticleSearchModel searchModel)
    {
        ArticleCategories = new SelectList(_articleCategoryApplication.GetCategories(), "Id", "Name");
        Articles = _articleApplication.Search(searchModel);
    }
}