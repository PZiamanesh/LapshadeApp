using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory;

public class IndexModel : PageModel
{
    private readonly IProductCategoryApplication _productCategoryApplication;
    public IEnumerable<ProductCategoryViewModel>? ProductCategories { get; set; }
    public ProductCategorySearchViewModel? SearchModel { get; set; }

    public IndexModel(IProductCategoryApplication productCategoryApplication)
    {
        _productCategoryApplication = productCategoryApplication;
    }

    public void OnGet(ProductCategorySearchViewModel searchModel)
    {
        ProductCategories = _productCategoryApplication.Search(searchModel);
    }
}