using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product;

public class IndexModel : PageModel
{
    private readonly IProductApplication _productApplication;
    private readonly IProductCategoryApplication _productCategoryApplication;

    public IEnumerable<ProductViewModel>? Products { get; set; }
    public ProductSearchViewModel? SearchModel { get; set; }
    public SelectList? ProductCategories { get; set; }

    public IndexModel(IProductApplication productApplication, 
        IProductCategoryApplication productCategoryApplication)
    {
        _productApplication = productApplication;
        _productCategoryApplication = productCategoryApplication;
    }

    public void OnGet(ProductSearchViewModel searchModel)
    {
        ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        Products = _productApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var createProduct = new CreateProduct() { ProductCategories = _productCategoryApplication.GetProductCategories() };
        return Partial("./Create", createProduct);
    }

    public async Task<JsonResult> OnPostCreate(CreateProduct command)
    {
        var result = await _productApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["ProductSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var product = _productApplication.GetDetails(id);
        product.ProductCategories = _productCategoryApplication.GetProductCategories();
        return Partial("./Edit", product);
    }

    public async Task<IActionResult> OnPostEdit(EditProduct command)
    {
        var result = await _productApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ProductEdition"] = result.Message;
        }
        return new JsonResult(result);
    }
}