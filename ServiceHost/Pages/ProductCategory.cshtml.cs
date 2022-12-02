using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class ProductCategoryModel : PageModel
{
    private readonly IProductCategoryQuery _productCategoryQuery;
    public ProductCategoryQueryModel ProductCategory { get; set; }

    public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public void OnGet(string id)
    {
        ProductCategory = _productCategoryQuery.GetProductCategoryWithProducts(id);
    }
}
