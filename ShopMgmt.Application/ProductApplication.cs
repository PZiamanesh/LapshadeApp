using _Framework.Application;
using ShopMgmt.Application.Contract.Product;
using ShopMgmt.Domain.ProductAgg;

namespace ShopMgmt.Application;
#nullable disable

public class ProductApplication : IProductApplication
{
    private readonly IProductRepository _productRepository;
    private readonly IFileUploader _fileUploader;

    public ProductApplication(IProductRepository productRepository,
        IFileUploader fileUploader)
    {
        _productRepository = productRepository;
        _fileUploader = fileUploader;
    }

    public async Task<OperationResult> Create(CreateProduct command)
    {
        var result = new OperationResult();
        if (_productRepository.Exists(p => p.Name == command.Name))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;
        string categorySlug = _productRepository.GetProductCategorySlug(command.CategoryId).Category;
        string path = Path.Combine(categorySlug, slug);
        var picturePath = await _fileUploader.Upload(command.Picture, path);

        var product = new Product(
            command.Name,
            command.Code,
            command.ShortDescription,
            command.Description,
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            slug, command.Keywords,
            command.MetaDescription,
            command.CategoryId
            );

        _productRepository.Create(product);
        _productRepository.Save();
        return result.Succeeded();
    }

    public async Task<OperationResult> Edit(EditProduct command)
    {
        
        var result = new OperationResult();
        var product = _productRepository.GetProductWithAncestors(command.Id);

        if (product is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productRepository.Exists(p => p.Name == command.Name && p.Id != command.Id))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug?.Slugify() ?? ApplicationMessage.NoSlug;
        string path = Path.Combine(product.Category.Slug, slug);
        var picturePath = await _fileUploader.Upload(command.Picture, path);

        product.Edit(
            command.Name,
            command.Code,
            command.ShortDescription,
            command.Description,
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            command.Slug,
            command.Keywords,
            command.MetaDescription);

        _productRepository.Save();
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

    public IEnumerable<ProductViewModel> GetProducts()
    {
        return _productRepository.GetProducts();
    }
}
