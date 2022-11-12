namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public record CreateProductPicture
{
    public long ProductId { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
}
