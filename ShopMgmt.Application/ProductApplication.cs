using _Framework.Application;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductAgg;

namespace ShopMgmt.Application;

public class ProductApplication : IProductApplication
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductApplication(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Create(CreateProduct command)
    {
        _unitOfWork.BeginTrans();
        var result = new OperationResult();
        if (_productRepository.Exists(p => p.Name == command.Name))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;

        var product = new Product(
            command.Name, command.Code, command.UnitPrice, command.ShortDescription,
            command.Description, command.Picture, command.PictureAlt, command.PictureTitle,
            slug, command.Keywords, command.MetaDescription, command.CategoryId
            );

        _productRepository.Create(product);
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult DeleteStock(long id)
    {
        _unitOfWork.BeginTrans();
        var result = new OperationResult();

        var product = _productRepository.Get(id);
        if (product is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        product.OutOfStock();
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult AddStock(long id)
    {
        _unitOfWork.BeginTrans();
        var result = new OperationResult();

        var product = _productRepository.Get(id);
        if (product is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        product.HaveInStock();
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Edit(EditProduct command)
    {
        _unitOfWork.BeginTrans();
        var result = new OperationResult();

        var product = _productRepository.Get(command.Id);

        if (product is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productRepository.Exists(p => p.Name == command.Name && p.Id != command.Id))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;
        product.Edit(
            command.Name, command.Code, command.UnitPrice, command.ShortDescription,
            command.Description, command.Picture, command.PictureAlt, command.PictureTitle,
            command.Slug, command.Keywords, command.MetaDescription);

        _unitOfWork.Commit();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditProduct GetDetails(long id)
    {
        return _productRepository.GetDetails(id);
    }

    public IEnumerable<ProductViewModel> Search(ProductSearchViewModel searchModel)
    {
        return _productRepository.Search(searchModel);
    }
}
