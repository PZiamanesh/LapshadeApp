namespace ShopMgmt.Application.Cotracts.ProductCategory;

public interface IProductCategoryApplication
{
    void Create(CreateProductCategory command);
    IEnumerable<ProductCategoryViewModel> Search(ProductCategorySearchViewModel model);
    void Edit(EditProductCategory command);
    EditProductCategory GetDetails(long id);
}
