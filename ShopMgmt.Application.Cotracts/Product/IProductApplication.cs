using _Framework.Application;
using ShopMgmt.Application.Contract.ProductCategory;

namespace ShopMgmt.Application.Contract.Product;

public interface IProductApplication
{
    OperationResult Create(CreateProduct command);

    OperationResult Edit(EditProduct command);

    EditProduct GetDetails(long id);

    OperationResult AddStock(long id);

    OperationResult DeleteStock(long id);

    IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel);
}
