using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Application.Contract.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture;
#nullable disable

public class IndexModel : PageModel
{
    private readonly IProductPictureApplication _productPictureApplication;
    private readonly IProductApplication _productApplication;

    public IEnumerable<ProductPictureViewModel> ProductPictures { get; set; }
    public ProductPictureSearchViewModel SearchModel { get; set; }
    public SelectList Products { get; set; }

    public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
    {
        _productPictureApplication = productPictureApplication;
        _productApplication = productApplication;
    }

    public void OnGet(ProductPictureSearchViewModel searchModel)
    {
        var products = _productApplication.GetProducts();
        Products = new SelectList(products, "Id", "Name");
        ProductPictures = _productPictureApplication.Search(searchModel);
    }

    public PartialViewResult OnGetCreate()
    {
        var products = _productApplication.GetProducts();
        var modelWithOnlyProducts = new CreateProductPicture() { Products = products };
        return Partial("Create", modelWithOnlyProducts);
    }

    public JsonResult OnPostCreate(CreateProductPicture command)
    {
        var result = _productPictureApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["ProductPictureSubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var productPicture = _productPictureApplication.GetDetails(id);
        productPicture.Products = _productApplication.GetProducts();
        return Partial("Edit", productPicture);
    }

    public JsonResult OnPostEdit(EditProductPicture command)
    {
        var result = _productPictureApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ProductPictureEdition"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        _productPictureApplication.Remove(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        _productPictureApplication.Restore(id);
        return RedirectToPage("./Index");
    }
}