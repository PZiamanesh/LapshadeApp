namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public record EditProductPicture : CreateProductPicture
{
    public long Id { get; set; }
}
