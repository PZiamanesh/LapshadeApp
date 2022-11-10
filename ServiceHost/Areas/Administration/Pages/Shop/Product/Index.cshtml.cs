using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product;

public class IndexModel : PageModel
{
    private readonly IProductApplication _productApplication;
    public IEnumerable<ProductViewModel>? Products { get; set; }
    public ProductSearchViewModel? SearchModel { get; set; }
    public SelectList? ProductCategories { get; set; }

    public IndexModel(IProductApplication productApplication)
    {
        _productApplication = productApplication;
    }

    public void OnGet(ProductSearchViewModel searchModel)
    {
        ProductCategories = new SelectList(_productApplication.GetProductCategories(), "Id", "Name");
        Products = _productApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        return Partial("./Create", new CreateProduct());
    }

    public JsonResult OnPostCreate(CreateProduct command)
    {
        var result = _productApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["ProductSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var product = _productApplication.GetDetails(id);
        return Partial("./Edit", product);
    }

    public IActionResult OnPostEdit(EditProduct command)
    {
        var result = _productApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ProductEdition"] = result.Message;
        }
        return new JsonResult(result);
    }
}