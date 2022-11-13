using _Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public record CreateProductPicture
{
    [Range(1,1000_000, ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Picture { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureTitle { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }
}
