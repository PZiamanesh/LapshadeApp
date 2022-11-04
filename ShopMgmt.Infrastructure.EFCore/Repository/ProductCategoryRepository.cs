using System.Linq.Expressions;
using _Framework.Infrastructure;
using ShopMgmt.Application.Contracts.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
{
    private readonly LapShadeDbContext _dbContext;

    public ProductCategoryRepository(LapShadeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model)
    {
        var query =
            _dbContext.ProductCategories?
                .Select(x => new ProductCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    ProductsCount = 0
                });

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            query = query?.Where(x => x.Name!
                .Contains(model.Name, StringComparison.InvariantCultureIgnoreCase));
            return query;
        }

        return query?.ToList();
    }
}