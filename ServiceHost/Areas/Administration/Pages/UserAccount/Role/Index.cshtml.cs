using AccountMgmt.Application.Contract.Role;
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
}