using _LampshadeQuery.Contract.ArticleCategory;
using _LampshadeQuery.Contract.Menu;
using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;
#nullable disable

public class MenuViewComponent : ViewComponent
{
    private readonly IProductCategoryQuery _productCategoryQuery;
    private readonly IArticleCategoryQuery _articleCategoryQuery;

    public MenuQueryModel Menu { get; private set; }

    public MenuViewComponent(IProductCategoryQuery productCategoryQuery,
        IArticleCategoryQuery articleCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
        _articleCategoryQuery = articleCategoryQuery;
    }

    public IViewComponentResult Invoke()
    {
        Menu = new MenuQueryModel
        {
            ProductCategories = _productCategoryQuery.GetProductCategories().ToList(),
            ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
        };

        return View(Menu);
    }
}
