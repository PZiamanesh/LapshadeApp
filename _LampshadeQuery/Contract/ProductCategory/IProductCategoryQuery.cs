namespace _LampshadeQuery.Contract.ProductCategory;

public interface IProductCategoryQuery
{
    IEnumerable<ProductCategoryQueryViewModel> GetProductCategories();
}