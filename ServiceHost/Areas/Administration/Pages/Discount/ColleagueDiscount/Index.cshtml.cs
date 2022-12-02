using DiscountMgmt.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount;

public class IndexModel : PageModel
{
    private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
    private readonly IProductApplication _productApplication;

    public IEnumerable<ColleagueDiscountViewModel>? ColleagueDiscounts { get; set; }
    public ColleagueDiscountSearchModel? SearchModel { get; set; }
    public SelectList? Products { get; set; }

    public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication,
        IProductApplication productApplication)
    {
        _colleagueDiscountApplication = colleagueDiscountApplication;
        _productApplication = productApplication;
    }

    public void OnGet(ColleagueDiscountSearchModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
    }

    public IActionResult OnGetDefine()
    {
        var defineDiscount = new DefineColleagueDiscount() { Products = _productApplication.GetProducts() };
        return Partial("./Define", defineDiscount);
    }

    public JsonResult OnPostDefine(DefineColleagueDiscount command)
    {
        var result = _colleagueDiscountApplication.Define(command);
        if (result.IsSucceeded)
        {
            TempData["ColleagueDiscountSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var discount = _colleagueDiscountApplication.GetDetails(id);
        discount.Products = _productApplication.GetProducts();
        return Partial("./Edit", discount);
    }

    public IActionResult OnPostEdit(EditColleagueDiscount command)
    {
        var result = _colleagueDiscountApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ColleagueDiscountEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        _colleagueDiscountApplication.Remove(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        _colleagueDiscountApplication.Restore(id);
        return RedirectToPage("./Index");
    }
}