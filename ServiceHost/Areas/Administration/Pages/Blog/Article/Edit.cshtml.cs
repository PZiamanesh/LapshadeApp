using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article;
#nullable disable

public class EditModel : PageModel
{
    private readonly IArticleApplication _articleApplication;
    private readonly IArticleCategoryApplication _articleCategoryApplication;

    public EditArticle Command { get; set; }
    public SelectList ArticleCategories { get; set; }

    public EditModel(IArticleApplication articleApplication,
        IArticleCategoryApplication articleCategoryApplication)
    {
        _articleApplication = articleApplication;
        _articleCategoryApplication = articleCategoryApplication;
    }

    public void OnGet(long id)
    {
        Command = _articleApplication.GetDetails(id);
        ArticleCategories = new SelectList(_articleCategoryApplication.GetCategories(), "Id", "Name");
    }

    public async Task<IActionResult> OnPost(EditArticle command)
    {
        var result = await _articleApplication.Create(command);
        return RedirectToPage("./Index");
    }
}
