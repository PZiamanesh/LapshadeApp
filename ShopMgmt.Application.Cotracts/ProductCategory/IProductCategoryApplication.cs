﻿using _Framework.Application;

namespace ShopMgmt.Application.Contracts.ProductCategory;

public interface IProductCategoryApplication
{
    OperationResult Create(CreateProductCategory command);

    IEnumerable<ProductCategoryViewModel> Search(ProductCategorySearchViewModel model);

    OperationResult Edit(EditProductCategory command);

    EditProductCategory GetDetails(long id);
}
