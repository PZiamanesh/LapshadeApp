﻿using _LampshadeQuery.Contract.ProductCategory;
using ShopMgmt.Infrastructure.EFCore;

namespace _LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _context;

    public ProductCategoryQuery(ShopContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductCategoryQueryViewModel> GetProductCategories()
    {
        return _context.ProductCategories.Select(x => new ProductCategoryQueryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug
        }).ToList();
    }
}