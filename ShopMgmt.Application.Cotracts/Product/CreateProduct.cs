using _Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopMgmt.Application.Contract.ProductCategory;
using System.ComponentModel.DataAnnotations;

public record CreateProduct
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Name { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Code { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    [MaxFileSize(300 * 1024, ErrorMessage = ValidationMessage.PictureSize)]
    [FileType(new string[] {".jpeg", ".jpg", ".png"}, ErrorMessage = ValidationMessage.PictureType)]
    public IFormFile? Picture { get; set; }

    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Slug { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Keywords { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? MetaDescription { get; set; }

    [Range(1, 100_000, ErrorMessage = ValidationMessage.IsRequired)]
    public long CategoryId { get; set; }

    public IEnumerable<ProductCategoryViewModel>? ProductCategories { get; set; }
}
