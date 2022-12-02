using _Framework.Application;

namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public interface IProductPictureApplication
{
    Task<OperationResult> Create(CreateProductPicture command);
    Task<OperationResult> Edit(EditProductPicture command);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditProductPicture GetDetails(long id);
    IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
}
