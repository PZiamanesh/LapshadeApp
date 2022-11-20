﻿using _Framework.Application;
using ShopMgmt.Application.Contract.ProductCategory;
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
        _productCategoryRepository.Save();
        return operationResult.Succeeded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        
        var operationResult = new OperationResult();
        var oldCategory = _productCategoryRepository.Get(command.Id);

        if (oldCategory == null)
        {
            return operationResult.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productCategoryRepository.Exists(x => x.Name!.Equals(command.Name) && x.Id != command.Id))
        {
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

        _productCategoryRepository.Save();
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

    public IEnumerable<ProductCategoryViewModel> GetProductCategories() => 
        _productCategoryRepository.GetProductCategories();
}