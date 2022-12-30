using AccountMgmt.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.UserAccount.Role;
#nullable disable

public class IndexModel : PageModel
{
    private readonly IRoleApplication _roleApplication;

    public List<RoleViewModel> Roles { get; set; }

    public IndexModel(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }

    public void OnGet()
    {
        Roles = _roleApplication.GetRoles();
    }

    public IActionResult OnGetCreate()
    {
        var model = new CreateRole();
        return Partial("./Create", model);
    }

    public JsonResult OnPostCreate(CreateRole command)
    {
        var result = _roleApplication.Create(command);

        if (result.IsSucceeded)
        {
            TempData["RoleSubmission"] = result.Message;
        }

        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var account = _roleApplication.GetDetails(id);
        return Partial("./Edit", account);
    }

    public IActionResult OnPostEdit(EditRole command)
    {
        var result = _roleApplication.Edit(command);

        if (result.IsSucceeded)
        {
            TempData["RoleEdition"] = result.Message;
        }

        return new JsonResult(result);
    }
}