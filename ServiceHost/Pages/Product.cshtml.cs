using _LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;

public class ProductModel : PageModel
{
    private readonly IProductQuery _productQuery;
    public ProductQueryModel? Product { get; set; }

    public ProductModel(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public void OnGet(string id)
    {
        Product = _productQuery.GetProduct(id);
    }
}
