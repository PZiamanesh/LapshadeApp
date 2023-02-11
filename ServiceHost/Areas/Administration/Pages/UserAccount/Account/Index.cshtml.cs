using _Framework.Application;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.UserAccount.Account;
#nullable disable

public class IndexModel : PageModel
{
    private readonly IAccountApplication _accountApplication;
    private readonly IRoleApplication _roleApplication;

    public List<AccountViewModel> Accounts { get; set; }
    public AccountSearchModel SearchModel { get; set; }
    public SelectList Roles { get; set; }

    public IndexModel(IAccountApplication accountApplication,
        IRoleApplication roleApplication)
    {
        _accountApplication = accountApplication;
        _roleApplication = roleApplication;
    }

    public void OnGet(AccountSearchModel searchModel)
    {
        Roles = new SelectList(_roleApplication.GetRoles(), "Id", "Name");
        Accounts = _accountApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var roles = _roleApplication.GetRoles();
        var model = new CreateAccount() { Roles = roles};
        return Partial("./Create", model);
    }

    public async Task<JsonResult> OnPostCreate(CreateAccount command)
    {
        if (!ModelState.IsValid)
        {
            var failed = new OperationResult();
            failed.Message = "پر کردن تمام فیلدها الزامی است";
            return new JsonResult(failed);
        }
        var result = await _accountApplication.Register(command);

        if (result.IsSucceeded)
        {
            TempData["AccountSubmission"] = result.Message;
        }

        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var account = _accountApplication.GetDetails(id);
        var roles = _roleApplication.GetRoles();
        account.Roles = roles;
        return Partial("./Edit", account);
    }

    public async Task<IActionResult> OnPostEdit(EditAccount command)
    {
        var result = await _accountApplication.Edit(command);

        if (result.IsSucceeded)
        {
            TempData["AccountEdition"] = result.Message;
        }

        return new JsonResult(result);
    }

    public IActionResult OnGetChangePassword(long id)
    {
        var passwdModel = new ChangePassword() { Id = id };
        return Partial("ChangePassword", passwdModel);
    }

    public IActionResult OnPostChangePassword(ChangePassword command)
    {
        var result = _accountApplication.ChangePassword(command);

        if (result.IsSucceeded)
        {
            TempData["PasswordChangeStatus"] = result.Message;
        }

        return new JsonResult(result);
    }
}