using ShopMgmt.Application.Contracts.ProductCategory;
using System.Linq.Expressions;

namespace ShopMgmt.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository
{
    void Create(ProductCategory category);

    ProductCategory? Get(long id);

    IEnumerable<ProductCategory> GetAll();

    bool Exists(Expression<Func<ProductCategory, bool>> expression);

    IEnumerable<ProductCategoryViewModel> Search(ProductCategorySearchViewModel model);

    void Save();
}
