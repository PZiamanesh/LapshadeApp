using _LampshadeQuery.Contract.Product;
using _LampshadeQuery.Contract.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Infrastructure.EFCore;

namespace _LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _context;

    public ProductCategoryQuery(ShopContext context)
    {
        _context = context;
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
        var categoriesWithTheirProducts = _context.ProductCategories
            .Include(x => x.Products)
                .ThenInclude(x=>x.Category)
            .Select(x => new ProductCategoryQueryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).ToList();

        return categoriesWithTheirProducts;
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
