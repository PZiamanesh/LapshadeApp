using AccountMgmt.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class AccountModel : PageModel
{
    private readonly IAccountApplication _accountApplication;

    public AccountModel(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPostLogin(Login command)
    {
        var result = _accountApplication.Login(command);

        if (!result.IsSucceeded)
        {
            ViewData["LoginErr"] = result.Message;
            ModelState.Clear();
            return Page();
        }

        return RedirectToPage("/Index");
    }

    public ActionResult OnGetLogout()
    {
        _accountApplication.Logout();
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostRegister(CreateAccount command)
    {

        var result = await _accountApplication.Register(command);

        if (!result.IsSucceeded)
        {
            ViewData["RegisterErr"] = result.Message;
            return Page();
        }

        return RedirectToPage("/Index");
    }
}
