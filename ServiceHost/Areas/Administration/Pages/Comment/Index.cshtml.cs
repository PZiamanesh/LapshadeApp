using CommentMgmt.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment;
#nullable disable

public class IndexModel : PageModel
{
    private readonly ICommentApplication _commentApplication;
    public List<CommentViewModel> Comments { get; set; }
    public CommentSearchModel SearchModel { get; set; }

    public IndexModel(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public void OnGet(CommentSearchModel searchModel)
    {
        Comments = _commentApplication.Search(searchModel);
    }
   
    public IActionResult OnGetConfirm(long id)
    {
        _commentApplication.Confirm(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetCancel(long id)
    {
        _commentApplication.Cancel(id);
        return RedirectToPage("./Index");
    }
}