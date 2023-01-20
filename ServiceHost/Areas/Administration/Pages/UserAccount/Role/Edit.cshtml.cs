using AccountMgmt.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.UserAccount.Role;
#nullable disable

public class EditModel : PageModel
{
    private readonly IRoleApplication _roleApplication;
    public EditRole Command { get; set; }

    public EditModel(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public void OnGet(long id)
    {
        Command = _roleApplication.GetDetails(id);
    }

    public IActionResult OnPost(EditRole command)
    {
        var result = _roleApplication.Edit(command);

        if (result.IsSucceeded)
        {
            TempData["RoleEdition"] = result.Message;
        }

        return RedirectToPage("./Index");
    }
}
