using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    private readonly IProductCategoryQuery _productCategoryQuery;

    public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _productCategoryQuery.GetProductCategories();
        return View(categories);
    }
}
