using _Framework.Domain;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ShopMgmt.Domain.ProductAgg;

public interface IProductRepository : IRepository<long, Product>
{
    EditProduct GetDetails(long id);

    IEnumerable<ProductViewModel> GetProducts();

    Product GetProductWithAncestors(long id);

    ProductViewModel GetProductCategorySlug(long id);

    IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel);
}
