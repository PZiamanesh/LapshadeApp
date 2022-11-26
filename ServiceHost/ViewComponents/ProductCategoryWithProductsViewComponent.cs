using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;

public class ProductCategoryWithProductsViewComponent : ViewComponent
{
    private readonly IProductCategoryQuery _productCategoryQuery;

    public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public IViewComponentResult Invoke()
    {
        var categoriesWithProducts = _productCategoryQuery.GetProductCategoriesWithProducts();
        return View(categoriesWithProducts);
    }
}
