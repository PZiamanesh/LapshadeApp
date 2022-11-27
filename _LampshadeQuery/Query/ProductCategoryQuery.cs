using _Framework.Application;
using _LampshadeQuery.Contract.Product;
using _LampshadeQuery.Contract.ProductCategory;
using DiscountMgmt.Infrastructure.EFCore;
using InventoryMgmt.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Infrastructure.EFCore;

namespace _LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;

    public ProductCategoryQuery(ShopContext context,
        InventoryContext inventoryContext,
        DiscountContext discountContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
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
        }).AsNoTracking().ToList();
    }

    public IEnumerable<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts()
    {
        // get inventory and customer discount list
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice, currentCount = x.CalculateCurrentCount() });

        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate >= DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate });


        // create categories with their products
        var categories = _context.ProductCategories
            .Include(x => x.Products)
                .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).AsNoTracking().ToList();


        // filling price and discount for each product of a category
        foreach (var category in categories)
        {
            foreach (var product in category.Products)
            {
                double price = 0.0;

                // get price
                var productInventory = inventory
                    .FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory is not null)
                {
                    if (productInventory.currentCount > 0)
                    {
                        price = productInventory.UnitPrice;
                        product.Price = productInventory.UnitPrice.ToMoney() + " تومان";
                    }
                    else
                    {
                        product.Price = "ناموجود";
                    }
                }
                else
                {
                    product.Price = "بزودی";
                }

                // get discount
                var productWithDiscount = customerDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id);

                if (productWithDiscount is not null)
                {
                    int discountRate = productWithDiscount.DiscountRate;
                    double discountAmount = Math.Round(discountRate * price / 100.0);
                    double discountedPrice = price - discountAmount;

                    product.DiscountRate = $"% {discountRate}-";
                    product.PriceWithDiscount = discountedPrice.ToMoney();
                    product.HasDiscount = true;
                }
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
            PictureTitle = x.PictureTitle,
            Name = x.Name,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug,
        }).ToList();
    }

    public ProductCategoryQueryViewModel GetProductCategoryWithProducts(string id)
    {
        // get inventory and customer discount list
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice, currentCount = x.CalculateCurrentCount() });

        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate >= DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate });


        // create category with their products
        var category = _context.ProductCategories
            .Include(x => x.Products)
                .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Products = MapProducts(x.Products),
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug
            }).AsNoTracking().FirstOrDefault(x => x.Slug == id);


        // filling price and discount for each product of a category
        foreach (var product in category.Products)
        {
            // get price
            double price = 0.0;
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory is not null)
            {
                if (productInventory.currentCount > 0)
                {
                    price = productInventory.UnitPrice;
                    product.Price = productInventory.UnitPrice.ToMoney() + " تومان";
                }
                else
                {
                    product.Price = "ناموجود";
                }
            }
            else
            {
                product.Price = "بزودی";
            }

            // get discount
            var productWithDiscount = customerDiscounts
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productWithDiscount is not null)
            {
                int discountRate = productWithDiscount.DiscountRate;
                double discountAmount = Math.Round(discountRate * price / 100.0);
                double discountedPrice = price - discountAmount;

                product.DiscountRate = $"% {discountRate}-";
                product.PriceWithDiscount = discountedPrice.ToMoney();
                product.HasDiscount = true;
                product.DiscountExpirationDate = productWithDiscount.EndDate.ToString("yyyy/MM/dd");
            }
        }

        return category;
    }
}
