using ShopMgmt.Domain.ProductPictureAggr;

namespace _LampshadeQuery.Contract.Product;

public class ProductQueryViewModel
{
    public long Id { get; set; }
    public string Category { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Slug { get; set; }
    public string Price { get; set; }
    public string PriceWithDisount { get; set; }
    public string DiscountRate { get; set; }
    public bool HasDiscount { get; set; }
}
