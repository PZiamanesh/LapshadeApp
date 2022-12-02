using _Framework.Application;

namespace ShopMgmt.Application.Contract.Product;

public interface IProductApplication
{
    Task<OperationResult> Create(CreateProduct command);

    Task<OperationResult> Edit(EditProduct command);

    EditProduct GetDetails(long id);

    IEnumerable<ProductViewModel> Search(ProductSearchModel searchModel);

    IEnumerable<ProductViewModel> GetProducts();
}
