using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article;
#nullable disable

public class CreateModel : PageModel
{
    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public CreateArticle Command { get; set; }
    public SelectList ArticleCategories { get; set; }

    public CreateModel(IArticleCategoryApplication articleCategoryApplication, 
        IArticleApplication articleApplication)
    {
        _articleCategoryApplication = articleCategoryApplication;
        _articleApplication = articleApplication;
    }

    public void OnGet()
    {
        ArticleCategories = new SelectList(_articleCategoryApplication.GetCategories(), "Id", "Name");
    }

    public async Task<IActionResult> OnPost(CreateArticle command)
    {
        var result = await _articleApplication.Create(command);
        return RedirectToPage("./Index");
    }
}
