using _Framework.Application;

namespace ShopMgmt.Application.Contract.ProductCategory;

public interface IProductCategoryApplication
{
    OperationResult Create(CreateProductCategory command);

    IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model);

    IEnumerable<ProductCategoryViewModel> GetProductCategories();

    OperationResult Edit(EditProductCategory command);

    EditProductCategory GetDetails(long id);
}
