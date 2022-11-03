using _Framework.Application;
using ShopMgmt.Application.Contracts.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ShopMgmt.Application;

public class ProductCategoryApplication : IProductCategoryApplication
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public OperationResult Create(CreateProductCategory command)
    {
        var operationResult = new OperationResult();

        if (_productCategoryRepository.Exists(x => x.Name == command.Name))
        {
            return operationResult.Failed("نام وارد شده از قبل موجود می باشد، لطفا نام دیگری انتخاب کنید.");
        }

        var slug = command.Slug?.Slugify() ?? "product_category_title_description";

        var productCategory = new ProductCategory(command.Name,
            command.Description,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Keywords,
            command.MetaDescription,
            slug);

        _productCategoryRepository.Create(productCategory);
        return operationResult.Success();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        var operationResult = new OperationResult();
        var oldCategory = _productCategoryRepository.Get(command.Id);

        if (oldCategory == null)
        {
            return operationResult.Failed("رکوردی با این مشخصات پیدا نشد، لطفا مجدداً تلاش کنید.");
        }

        if (_productCategoryRepository.Exists(x => x.Name!.Equals(command.Name) && x.Id != command.Id))
        {
            return operationResult.Failed("نام وارد شده از قبل موجود می باشد، لطفا نام دیگری انتخاب کنید.");
        }

        var slug = command.Slug?.Slugify() ?? "product_category_title_description";

        oldCategory.Edit(command.Name,
            command.Description,
            command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Keywords,
            command.MetaDescription,
            slug);

        _productCategoryRepository.Save();
        return operationResult.Success();
    }

    public EditProductCategory GetDetails(long id)
    {
        var productCategory = _productCategoryRepository.Get(id)!;
        return new EditProductCategory()
        {
            Description = productCategory.Description,
            Id = productCategory.Id,
            Keywords = productCategory.Keywords,
            MetaDescription = productCategory.MetaDescription,
            Picture = productCategory.Picture,
            PictureAlt = productCategory.PictureAlt,
            PictureTitle = productCategory.PictureTitle,
            Name = productCategory.Name,
            Slug = productCategory.Slug
        };
    }

    public IEnumerable<ProductCategoryViewModel> Search(ProductCategorySearchViewModel model)
    {
        return _productCategoryRepository.Search(model);
    }
}