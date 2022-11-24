using _Framework.Application;
using _LampshadeQuery.Contract.Product;
using _LampshadeQuery.Contract.ProductCategory;
using InventoryMgmt.Domain.InventoryAgg;
using InventoryMgmt.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Infrastructure.EFCore;
using System.Globalization;

namespace _LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;

    public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
    }

    public IEnumerable<ProductCategoryQueryViewModel> GetProductCategories()
    {
        return _context.ProductCategories.Select(x => new ProductCategoryQueryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug
        }).ToList();
    }

    public IEnumerable<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts()
    {
        var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice });

        var categories = _context.ProductCategories
            .Include(x => x.Products)
                .ThenInclude(x=>x.Category)
            .Select(x => new ProductCategoryQueryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).ToList();

        foreach (var category in categories)
        {
            foreach (var product in category.Products)
            {
                product.Price = inventory
                    .FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice.ToMoney() ?? "عدم تعریف در انبار";
            }
        }

        return categories;
    }

    private static List<ProductQueryViewModel> MapProducts(List<Product> products)
    {
        return products.Select(x => new ProductQueryViewModel()
        {
            Id = x.Id,
            Category = x.Category.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle= x.PictureTitle,
            Name = x.Name,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription
        }).ToList();
    }
}
