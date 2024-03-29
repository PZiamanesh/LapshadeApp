﻿using _Framework.Domain;
using ShopMgmt.Domain.ProductCategoryAgg;
using ShopMgmt.Domain.ProductPictureAggr;

namespace ShopMgmt.Domain.ProductAgg;
#nullable disable

public class Product : EntityBase<long>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string ShortDescription { get; private set; }
    public string Description { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public string Slug { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }

    public long CategoryId { get; private set; }
    public ProductCategory Category { get; private set; }

    public ICollection<ProductPicture> Pictures { get; private set; }

    protected Product()
    {
        Pictures = new List<ProductPicture>();
    }

    public Product(
        string name,
        string code,
        string shortDescription,
        string description,
        string picture,
        string pictureAlt,
        string pictureTitle,
        string slug,
        string keywords,
        string metaDescription,
        long categoryId)
    {
        Name = name;
        Code = code;
        ShortDescription = shortDescription;
        Description = description;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CategoryId = categoryId;
    }

    public void Edit(
        string name,
        string code,
        string shortDescription,
        string description,
        string picture,
        string pictureAlt,
        string pictureTitle,
        string slug,
        string keywords,
        string metaDescription)
    {
        Name = name;
        Code = code;
        ShortDescription = shortDescription;
        Description = description;

        if (!string.IsNullOrWhiteSpace(picture))
        {
            Picture = picture;
        }

        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
    }
}
