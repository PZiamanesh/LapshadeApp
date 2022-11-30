using _Framework.Application;
using ShopMgmt.Application.Contract.ProductPicture;
using ShopMgmt.Domain.ProductAgg;
using ShopMgmt.Domain.ProductPictureAggr;

namespace ShopMgmt.Application;
#nullable disable

public class ProductPictureApplication : IProductPictureApplication
{
    private readonly IProductPictureRepository _productPictureRepository;
    private readonly IProductRepository _productRepository;
    private readonly IFileUploader _fileUploader;

    public ProductPictureApplication(IProductPictureRepository productPictureRepository,
        IProductRepository productRepository,
        IFileUploader fileUploader)
    {
        _productPictureRepository = productPictureRepository;
        _productRepository = productRepository;
        _fileUploader = fileUploader;
    }

    public async Task<OperationResult> Create(CreateProductPicture command)
    {
        var result = new OperationResult();

        var product = _productRepository.GetProductWithAncestors(command.ProductId);
        var filePath = Path.Combine(product.Category.Slug, product.Slug);
        var picturePath = await _fileUploader.Upload(command.Picture, filePath);

        var picture = new ProductPicture(
            command.ProductId,
            picturePath,
            command.PictureAlt,
            command.PictureTitle
            );

        _productPictureRepository.Create(picture);
        _productPictureRepository.Save();
        return result.Succeeded();
    }

    public async Task<OperationResult> Edit(EditProductPicture command)
    {
        var result = new OperationResult();
        var picture = _productPictureRepository.GetProductPictureWithAncestors(command.Id);

        if (picture is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        var filePath = Path.Combine(picture.Product.Category.Slug, picture.Product.Slug);
        var picturePath = await _fileUploader.Upload(command.Picture, filePath);

        picture.Edit(
            command.ProductId,
            picturePath,
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
