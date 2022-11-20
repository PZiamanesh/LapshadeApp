using _Framework.Application;
using ShopMgmt.Application.Contract.ProductPicture;
using ShopMgmt.Domain.ProductPictureAggr;

namespace ShopMgmt.Application;
#nullable disable

public class ProductPictureApplication : IProductPictureApplication
{
    private readonly IProductPictureRepository _productPictureRepository;

    public ProductPictureApplication(IProductPictureRepository productPictureRepository)
    {
        _productPictureRepository = productPictureRepository;
    }

    public OperationResult Create(CreateProductPicture command)
    {
        var result = new OperationResult();

        if (_productPictureRepository
            .Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var picture = new ProductPicture(
            command.ProductId,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle
            );

        _productPictureRepository.Create(picture);
        _productPictureRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var result = new OperationResult();
        var picture = _productPictureRepository.Get(command.Id);

        if (picture is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productPictureRepository
            .Exists(x => x.Picture == command.Picture
                && x.ProductId == command.ProductId
                && x.Id != command.Id))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        picture.Edit(
            command.ProductId,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle
            );

        _productPictureRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditProductPicture GetDetails(long id)
    {
        return _productPictureRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var result = new OperationResult();
        var picture = _productPictureRepository.Get(id);

        if (picture is null)
        {
            
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        picture.Remove();
        _productPictureRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        var picture = _productPictureRepository.Get(id);

        if (picture is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        picture.Restore();
        _productPictureRepository.Save();
        return result.Succeeded();
    }

    public IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchViewModel searchModel)
    {
        return _productPictureRepository.Search(searchModel);
    }
}
