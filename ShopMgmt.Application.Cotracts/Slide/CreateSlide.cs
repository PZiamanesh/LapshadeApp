using _Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopMgmt.Application.Contract.Slide;
#nullable disable

public record CreateSlide
{
    [MaxFileSize(300 * 1024, ErrorMessage = ValidationMessage.PictureSize)]
    [FileType(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessage.PictureType)]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Heading { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string BtnText { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Link { get; set; }
}
