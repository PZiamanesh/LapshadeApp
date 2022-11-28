using System.ComponentModel.DataAnnotations;
using System.Drawing;
using _Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopMgmt.Application.Contract.ProductCategory;

public class CreateProductCategory
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [MinLength(3, ErrorMessage = ValidationMessage.MinLength)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [MaxFileSize(30 * 1024, ErrorMessage = ValidationMessage.ProductPictureSizeLimit)]
    //[AllowedFileFormat()]
    public IFormFile? Picture { get; set; }

    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Keywords { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? MetaDescription { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string? Slug { get; set; }
}
