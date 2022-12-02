using _Framework.Application;

namespace ShopMgmt.Application.Contract.ProductCategory;

public interface IProductCategoryApplication
{
    Task<OperationResult> Create(CreateProductCategory command);

    IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchModel model);

    IEnumerable<ProductCategoryViewModel> GetProductCategories();

    Task<OperationResult> Edit(EditProductCategory command);

    EditProductCategory GetDetails(long id);
}
