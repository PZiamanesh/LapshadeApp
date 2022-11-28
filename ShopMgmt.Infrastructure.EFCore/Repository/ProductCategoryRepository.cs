using System.Globalization;
using _Framework.Application;
using _Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
{
    private readonly ShopContext _context;

    public ProductCategoryRepository(ShopContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public EditProductCategory GetDetails(long id)
    {
        var productCategory = _context.ProductCategories?.FirstOrDefault(x=>x.Id == id);
        return new EditProductCategory()
        {
            Description = productCategory!.Description,
            Id = productCategory.Id,
            Keywords = productCategory.Keywords,
            MetaDescription = productCategory.MetaDescription,
            //Picture = productCategory.Picture,
            PictureAlt = productCategory.PictureAlt,
            PictureTitle = productCategory.PictureTitle,
            Name = productCategory.Name,
            Slug = productCategory.Slug
        };
    }

    public IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model)
    {
        var query = (_context.ProductCategories ?? throw new InvalidOperationException(ApplicationMessage.RecordNotFound))
                .Select(x => new ProductCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductsCount = 0
                });

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            query = query.Where(x => x.Name!
                .Contains(model.Name));
            return query;
        }

        return query.OrderByDescending(x=>x.Id).ToList();
    }

    public IEnumerable<ProductCategoryViewModel> GetProductCategories()
    {
        return _context.ProductCategories!.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}