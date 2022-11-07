using System.Globalization;
using _Framework.Infrastructure;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
{
    private readonly LampShadeDbContext _dbContext;

    public ProductCategoryRepository(LampShadeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model)
    {
        var query = (_dbContext.ProductCategories ?? throw new InvalidOperationException("ProductCategories is null at source."))
                .Select(x => new ProductCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
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
}