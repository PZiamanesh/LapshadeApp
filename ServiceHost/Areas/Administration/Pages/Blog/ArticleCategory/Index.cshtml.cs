using _Framework.Application;
using BlogMgmt.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory;

public class IndexModel : PageModel
{
    private readonly IArticleCategoryApplication _articleCategoryApplication;
    public List<ArticleCategoryViewModel>? ArticleCategories { get; set; }
    public ArticleCategorySearchModel? SearchModel { get; set; }

    public IndexModel(IArticleCategoryApplication articleCategoryApplication)
    {
        _articleCategoryApplication = articleCategoryApplication;
    }

    public void OnGet(ArticleCategorySearchModel searchModel)
    {
        ArticleCategories = _articleCategoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        return Partial("./Create", new CreateArticleCategory());
    }

    public async Task<JsonResult> OnPostCreate(CreateArticleCategory command)
    {
        if (!ModelState.IsValid)
        {
            var failed = new OperationResult();
            failed.Message = "پر کردن تمام فیلدها الزامی است";
            return new JsonResult(failed);
        }
        var result = await _articleCategoryApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["ArticleCategorySubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var articleCategory = _articleCategoryApplication.GetDetails(id);
        return Partial("./Edit", articleCategory);
    }

    public async Task<IActionResult> OnPostEdit(EditArticleCategory command)
    {
        // validation in case client side fail on picture
        if (!ModelState.IsValid)
        {
            command.Picture = null;
        }

        var result = await _articleCategoryApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ArticleCategoryEdition"] = result.Message;
        }
        return new JsonResult(result);
    }
}