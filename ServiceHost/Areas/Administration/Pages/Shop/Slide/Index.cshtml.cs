using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopMgmt.Application.Contract.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide;
#nullable disable

public class IndexModel : PageModel
{
    private readonly ISlideApplication _slideApplication;
    public IEnumerable<SlideViewModel> Slides { get; set; }

    public IndexModel(ISlideApplication slideApplication)
    {
        _slideApplication = slideApplication;
    }

    public void OnGet()
    {
        Slides = _slideApplication.GetAll();
    }

    public PartialViewResult OnGetCreate()
    {
        return Partial("Create", new CreateSlide());
    }

    public async Task<JsonResult> OnPostCreate(CreateSlide command)
    {
        var result = await _slideApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["SlideSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var slide = _slideApplication.GetDetails(id);
        return Partial("Edit", slide);
    }

    public async Task<JsonResult> OnPostEdit(EditSlide command)
    {
        var result = await _slideApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["SlideEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        _slideApplication.Remove(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        _slideApplication.Restore(id);
        return RedirectToPage("./Index");
    }
}