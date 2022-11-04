using ShopMgmt.Application.Contracts.ProductCategory;
using System.Linq.Expressions;
using _Framework.Domain;

namespace ShopMgmt.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository : IRepository<long, ProductCategory>
{
    IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model);
}