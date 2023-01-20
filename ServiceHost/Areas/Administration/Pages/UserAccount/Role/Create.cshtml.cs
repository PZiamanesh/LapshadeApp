using AccountMgmt.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.UserAccount.Role;
#nullable disable

public class CreateModel : PageModel
{
    private readonly IRoleApplication _roleApplication;
    public CreateRole Command { get; set; } = new();

    public CreateModel(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public void OnGet()
    {
        
    }

    public IActionResult OnPost(CreateRole command)
    {
        var result = _roleApplication.Create(command);

        if (result.IsSucceeded)
        {
            TempData["RoleSubmission"] = result.Message;
        }

        return RedirectToPage("./Index");
    }
}
