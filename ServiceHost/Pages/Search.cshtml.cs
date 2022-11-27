using _LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class SearchModel : PageModel
{
    private readonly IProductQuery _productQuery;
    public IEnumerable<ProductQueryViewModel> Products { get; set; }
    public string SearchValue { get; set; }

    public SearchModel(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public IActionResult OnGet(string searchKey)
    {
        SearchValue = searchKey;
        if (string.IsNullOrWhiteSpace(searchKey))
        {
            return RedirectToPage("/EmptySearchItem");
        }

        Products = _productQuery.Search(searchKey);
        if (Products.Count() > 0)
        {
            return Page();
        }
        else
        {
            return RedirectToPage("/EmptySearchItem");
        }
    }
}
