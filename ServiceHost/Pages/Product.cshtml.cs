using _LampshadeQuery.Contract.Product;
using CommentMgmt.Application.Contract.Comment;
using CommentMgmt.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;
#nullable disable

public class ProductModel : PageModel
{
    private readonly IProductQuery _productQuery;
    private readonly ICommentApplication _commentApplication;
    public ProductQueryModel Product { get; set; }

    public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
    {
        _productQuery = productQuery;
        _commentApplication = commentApplication;
    }

    public void OnGet(string id)
    {
        Product = _productQuery.GetProduct(id);
    }

    public IActionResult OnPost(AddComment command, string productSlug)
    {
        command.EntityType = CommentType.Product;
        var result = _commentApplication.AddComment(command);
        if (result.IsSucceeded)
        {
            TempData["CommentStatus"] = "باتشکر. نظر شما بعد از تایید مدیر نمایش داده خواهد شد.";
        }
        return RedirectToPage("/Product", new {id = productSlug});
    }
}
