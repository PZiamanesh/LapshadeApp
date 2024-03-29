﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopMgmt.Application.Contract.ProductCategory;
using ShopMgmt.Domain.ProductCategoryAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory;

public class IndexModel : PageModel
{
    private readonly IProductCategoryApplication _productCategoryApplication;
    public IEnumerable<ProductCategoryViewModel>? ProductCategories { get; set; }
    public ProductCategorySearchModel? SearchModel { get; set; }

    public IndexModel(IProductCategoryApplication productCategoryApplication)
    {
        _productCategoryApplication = productCategoryApplication;
    }

    public void OnGet(ProductCategorySearchModel searchModel)
    {
        ProductCategories = _productCategoryApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        return Partial("./Create", new CreateProductCategory());
    }

    public async Task<JsonResult> OnPostCreate(CreateProductCategory command)
    {
        var result = await _productCategoryApplication.Create(command);
        if (result.IsSucceeded)
        {
            TempData["ProductCategorySubmission"] = result.Message;
        }
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var productCategory = _productCategoryApplication.GetDetails(id);
        return Partial("./Edit", productCategory);
    }

    public async Task<IActionResult> OnPostEdit(EditProductCategory command)
    {
        // validation in case client side fail on picture
        if (!ModelState.IsValid)
        {
            command.Picture = null;
        }

        var result = await _productCategoryApplication.Edit(command);
        if (result.IsSucceeded)
        {
            TempData["ProductCategoryEdition"] = result.Message;
        }
        return new JsonResult(result);
    }
}