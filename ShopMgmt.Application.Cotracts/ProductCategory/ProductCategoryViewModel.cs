﻿namespace ShopMgmt.Application.Cotracts.ProductCategory;

public class ProductCategoryViewModel
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public long ProductsCount { get; set; }
}