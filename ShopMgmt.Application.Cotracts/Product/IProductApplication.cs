using _Framework.Application;

namespace ShopMgmt.Application.Contract.Product;

public interface IProductApplication
{
    OperationResult Create(CreateProduct command);

    OperationResult Edit(EditProduct command);

    EditProduct GetDetails(long id);

    OperationResult AddStock(long id);

    OperationResult DeleteStock(long id);

    IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel);

    IEnumerable<ProductViewModel> GetProducts();
}
