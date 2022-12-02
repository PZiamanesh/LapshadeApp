using _Framework.Domain;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ShopMgmt.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository : IRepository<long, ProductCategory>
{
    EditProductCategory GetDetails(long id);
    IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchModel model);
    IEnumerable<ProductCategoryViewModel> GetProductCategories();
}