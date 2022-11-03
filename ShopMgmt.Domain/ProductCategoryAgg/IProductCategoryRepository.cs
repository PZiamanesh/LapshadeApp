namespace ShopMgmt.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository
{
    void Create(ProductCategory category);
    ProductCategory Get(long id);
    IEnumerable<ProductCategory> GetAll();
}
