using _Framework.Application;
using _Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Domain.ProductAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;

public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProduct GetDetails(long id)
    {
        var product = _context.Products?.FirstOrDefault(x => x.Id == id);

        return new EditProduct()
        {
            Id = product!.Id,
            Name = product.Name,
            Code = product.Code,
            UnitPrice = product.UnitPrice,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            Picture = product.Picture,
            PictureAlt = product.PictureAlt,
            PictureTitle = product.PictureTitle,
            Slug = product.Slug,
            Keywords = product.Keywords,
            MetaDescription = product.MetaDescription,
            CategoryId = product.CategoryId
        };
    }

    public IEnumerable<ProductViewModel> GetProducts()
    {
        return _context.Products!.Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel)
    {
        var query = _context.Products?
            .Include(x => x.Category)
            .Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                UnitPrice = x.UnitPrice,
                Picture = x.Picture,
                Category = x.Category!.Name,
                CategoryId = x.Category.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                IsStocked = x.InStock
            }) ?? throw new InvalidOperationException(ApplicationMessage.RecordNotFound);

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name!.Contains(searchModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Code))
        {
            query = query.Where(x => x.Code!.Contains(searchModel.Code));
        }

        if (searchModel.CategoryId > 0)
        {
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}
