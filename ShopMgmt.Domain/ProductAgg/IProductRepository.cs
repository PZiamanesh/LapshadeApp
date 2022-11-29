using _Framework.Domain;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ShopMgmt.Domain.ProductAgg;

public interface IProductRepository : IRepository<long, Product>
{
    EditProduct GetDetails(long id);

    IEnumerable<ProductViewModel> GetProducts();

    ProductViewModel GetProductCategorySlug(long categoryId); // category slug value included in Category prop

    IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel);
}
