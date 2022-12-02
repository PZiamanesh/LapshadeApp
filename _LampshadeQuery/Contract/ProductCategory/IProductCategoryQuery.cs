namespace _LampshadeQuery.Contract.ProductCategory;

public interface IProductCategoryQuery
{
    IEnumerable<ProductCategoryQueryModel> GetProductCategories();
    IEnumerable<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    ProductCategoryQueryModel GetProductCategoryWithProducts(string id);
}