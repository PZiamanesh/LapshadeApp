using _Framework.Application;
using ShopMgmt.Application.Contract.ProductPicture;
using ShopMgmt.Domain.ProductPictureAggr;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopMgmt.Application;
#nullable disable

public class ProductPictureApplication : IProductPictureApplication
{
    private readonly IProductPictureRepository _productPictureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductPictureApplication(IProductPictureRepository productPictureRepository, IUnitOfWork unitOfWork)
    {
        _productPictureRepository = productPictureRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Create(CreateProductPicture command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        if (_productPictureRepository
            .Exists(x=>x.Picture == command.Picture && x.ProductId == command.ProductId))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var picture = new ProductPicture(
            command.ProductId,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle
            );

        _productPictureRepository.Create(picture);

        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var picture = _productPictureRepository.Get(command.Id);
        if (picture is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productPictureRepository
            .Exists(x => x.Picture == command.Picture 
                && x.ProductId == command.ProductId
                && x.Id != command.Id))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        picture.Edit(
            command.ProductId,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle
            );

        _unitOfWork.Commit();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditProductPicture GetDetails(long id)
    {
        return _productPictureRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var picture = _productPictureRepository.Get(id);
        if (picture is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        picture.Remove();
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var picture = _productPictureRepository.Get(id);
        if (picture is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        picture.Restore();
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchViewModel searchModel)
    {
        return _productPictureRepository.Search(searchModel);
    }
}
