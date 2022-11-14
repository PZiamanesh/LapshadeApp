using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;
#nullable disable

public class ProductCategoryViewComponent : ViewComponent
{
    private readonly IProductCategoryQuery _productCategoryQuery;

    public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public IViewComponentResult Invoke()
    {
        var productCategories = _productCategoryQuery.GetProductCategories();
        return View(productCategories);
    }
}
