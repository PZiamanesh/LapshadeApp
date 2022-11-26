namespace _LampshadeQuery.Contract.ProductCategory;

public interface IProductCategoryQuery
{
    IEnumerable<ProductCategoryQueryViewModel> GetProductCategories();
    IEnumerable<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts();
    ProductCategoryQueryViewModel GetProductCategoryWithProducts(string id);
}