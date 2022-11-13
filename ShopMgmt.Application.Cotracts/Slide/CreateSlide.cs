using _Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopMgmt.Application.Contract.Slide;
#nullable disable

public record CreateSlide
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Picture { get; set; }

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
}
