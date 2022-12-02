using _Framework.Application;
using _LampshadeQuery.Contract.Product;
using DiscountMgmt.Infrastructure.EFCore;
using InventoryMgmt.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductPictureAggr;
using ShopMgmt.Infrastructure.EFCore;

namespace _LampshadeQuery.Query;

public class ProductQuery : IProductQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;

    public ProductQuery(ShopContext context,
        InventoryContext inventoryContext,
        DiscountContext discountContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }

    public IEnumerable<ProductQueryModel> GetLatestProducts()
    {
        // get inventory and customer discount list
        var inventory = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice,
                x.InStock,
                currentCount = x.CalculateCurrentCount()
            });

        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate >= DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate });

        var products = _context.Products
            .Include(x => x.Category)
            .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Name = x.Name,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
            }).OrderByDescending(x => x.Id).Take(6).AsNoTracking().ToList();


        foreach (var product in products)
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
                    product.InStock = productInventory.InStock;
                }
                else
                {
                    product.Price = "ناموجود";
                    product.InStock = productInventory.InStock;
                }
            }
            else
            {
                product.Price = "بزودی";
                product.InStock = false;
            }

            // get discount
            var productWithDiscount = customerDiscounts
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productWithDiscount is not null)
            {
                int discountRate = productWithDiscount.DiscountRate;
                double discountAmount = Math.Round(discountRate * price / 100.0);
                double discountedPrice = price - discountAmount;

                product.DiscountRate = $"-{discountRate}%";
                product.PriceWithDiscount = discountedPrice.ToMoney();
                product.HasDiscount = true;
            }
        }

        return products;
    }

    public ProductQueryModel GetProduct(string slug)
    {
        // get inventory and customer discount list
        var inventory = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice,
                x.InStock,
                currentCount = x.CalculateCurrentCount()
            });

        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate >= DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate });

        var product = _context.Products
            .Include(x => x.Category)
            .Include(x => x.Pictures)
            .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Code = x.Code,

                Category = x.Category.Name,
                CategorySlug = x.Category.Slug,

                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Pictures = MapProductPictures(x.Pictures),

                ShortDescription = x.ShortDescription,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords,
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

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
                product.InStock = productInventory.InStock;
            }
            else
            {
                product.Price = "ناموجود";
                product.InStock = productInventory.InStock;
            }
        }
        else
        {
            product.Price = "بزودی";
            product.InStock = false;
        }

        // get discount
        var productWithDiscount = customerDiscounts
            .FirstOrDefault(x => x.ProductId == product.Id);

        if (productWithDiscount is not null)
        {
            int discountRate = productWithDiscount.DiscountRate;
            double discountAmount = Math.Round(discountRate * price / 100.0);
            double discountedPrice = price - discountAmount;

            product.DiscountRate = $"-{discountRate}%";
            product.PriceWithDiscount = discountedPrice.ToMoney();
            product.DiscountExpirationDate = productWithDiscount.EndDate.ToString("yyyy/MM/dd");
            product.HasDiscount = true;
        }

        return product;
    }

    private static List<ProductPictureQueryModel> MapProductPictures(ICollection<ProductPicture> pictures)
    {
        return pictures.Select(x => new ProductPictureQueryModel
        {
            ProductId = x.ProductId,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            IsRemoved = x.IsRemoved,
        }).Where(x => !x.IsRemoved).ToList();
    }

    public IEnumerable<ProductQueryModel> Search(string searchKey)
    {
        // get inventory and customer discount list
        var inventory = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice,
                x.InStock,
                currentCount = x.CalculateCurrentCount()
            });

        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate >= DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate });


        // fill query item
        var products = _context.Products
            .Include(x => x.Category)
            .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Name = x.Name,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
            })
            .Where(p => p.Name.Contains(searchKey))
            .AsNoTracking()
            .ToList();

        foreach (var product in products)
        {
            // process price
            double price = 0.0;
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory is not null)
            {
                if (productInventory.currentCount > 0)
                {
                    price = productInventory.UnitPrice;
                    product.Price = productInventory.UnitPrice.ToMoney() + " تومان";
                    product.InStock = productInventory.InStock;
                }
                else
                {
                    product.Price = "ناموجود";
                    product.InStock = productInventory.InStock;
                }
            }
            else
            {
                product.Price = "بزودی";
                product.InStock = false;
            }

            // process discount
            var productWithDiscount = customerDiscounts
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productWithDiscount is not null)
            {
                int discountRate = productWithDiscount.DiscountRate;
                double discountAmount = Math.Round(discountRate * price / 100.0);
                double discountedPrice = price - discountAmount;

                product.DiscountRate = $"-{discountRate}%";
                product.PriceWithDiscount = discountedPrice.ToMoney();
                product.HasDiscount = true;
            }
        }

        return products;
    }
}

