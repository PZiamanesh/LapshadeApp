using _Framework.Application;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Application;

public class ProductCategoryApplication : IProductCategoryApplication
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, 
        IUnitOfWork unitOfWork)
    {
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Create(CreateProductCategory command)
    {
        _unitOfWork.BeginTrans();
        var operationResult = new OperationResult();

        if (_productCategoryRepository.Exists(x => x.Name == command.Name))
        {
            _unitOfWork.RollBack();
            return operationResult.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;

        var productCategory = new ProductCategory(command.Name,
            command.Description,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Keywords,
            command.MetaDescription,
            slug);

        _productCategoryRepository.Create(productCategory);
        _unitOfWork.Commit();
        return operationResult.Succeeded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        _unitOfWork.BeginTrans();
        var operationResult = new OperationResult();
        var oldCategory = _productCategoryRepository.Get(command.Id);

        if (oldCategory == null)
        {
            _unitOfWork.RollBack();
            return operationResult.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productCategoryRepository.Exists(x => x.Name!.Equals(command.Name) && x.Id != command.Id))
        {
            _unitOfWork.RollBack();
            return operationResult.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;

        oldCategory.Edit(command.Name,
            command.Description,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Keywords,
            command.MetaDescription,
            slug);

        _unitOfWork.Commit();
        return operationResult.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditProductCategory GetDetails(long id)
    {
        return _productCategoryRepository.GetDetails(id);
    }

    public IEnumerable<ProductCategoryViewModel>? Search(ProductCategorySearchViewModel model)
    {
        return _productCategoryRepository.Search(model);
    }

    public IEnumerable<ProductCategoryViewModel> GetProductCategories() => _productCategoryRepository.GetProductCategories();
}