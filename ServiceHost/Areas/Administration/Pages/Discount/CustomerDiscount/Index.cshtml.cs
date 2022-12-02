using DiscountMgmt.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount;

public class IndexModel : PageModel
{
    private readonly ICustomerDiscountApplication _colleagueDiscountApplication;
    private readonly IProductApplication _productApplication;

    public IEnumerable<CustomerDiscountViewModel>? CustomerDiscounts { get; set; }
    public CustomerDiscountSearchModel? SearchModel { get; set; }
    public SelectList? Products { get; set; }

    public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
    {
        _colleagueDiscountApplication = customerDiscountApplication;
        _productApplication = productApplication;
    }

    public void OnGet(CustomerDiscountSearchModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        CustomerDiscounts = _colleagueDiscountApplication.Search(searchModel);
    }

    public IActionResult OnGetDefine()
    {
        var defineDiscount = new DefineCustomerDiscount() { Products = _productApplication.GetProducts() };
        return Partial("./Define", defineDiscount);
    }

    public JsonResult OnPostDefine(DefineCustomerDiscount command)
    {
        var result = _colleagueDiscountApplication.Define(command);
        if (result.IsSucceeded)
        {
            TempData["CustomerDiscountSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var discount = _colleagueDiscountApplication.GetDetails(id);
        discount.Products = _productApplication.GetProducts();
        return Partial("./Edit", discount);
    }

    public IActionResult OnPostEdit(EditCustomerDiscount command)
    {
        var result = _colleagueDiscountApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["CustomerDiscountEdition"] = result.Message;
        }
        return new JsonResult(result);
    }
}