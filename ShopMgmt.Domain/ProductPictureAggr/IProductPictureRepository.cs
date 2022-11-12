using _Framework.Domain;
using ShopMgmt.Application.Contract.ProductPicture;

namespace ShopMgmt.Domain.ProductPictureAggr;
#nullable disable

public interface IProductPictureRepository : IRepository<long, ProductPicture>
{
    EditProductPicture GetDetails(long id);
    IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchViewModel searchModel);
}
