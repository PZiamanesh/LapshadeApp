using _Framework.Application;

namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public interface IProductPictureApplication
{
    OperationResult Create(CreateProductPicture command);
    OperationResult Edit(EditProductPicture command);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditProductPicture GetDetails(long id);
    IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchViewModel searchModel);
}
