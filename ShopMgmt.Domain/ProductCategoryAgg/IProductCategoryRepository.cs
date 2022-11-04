using _Framework.Domain;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ShopMgmt.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository : IRepository<long, ProductCategory>
{
    IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model);
}