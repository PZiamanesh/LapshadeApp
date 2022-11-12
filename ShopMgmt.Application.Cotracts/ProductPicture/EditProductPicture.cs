namespace ShopMgmt.Application.Contract.ProductPicture;

public record EditProductPicture : CreateProductPicture
{
    public long Id { get; set; }
}
