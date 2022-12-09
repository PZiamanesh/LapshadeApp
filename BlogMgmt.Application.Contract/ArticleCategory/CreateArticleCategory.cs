using _Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogMgmt.Application.Contract.ArticleCategory;

public record CreateArticleCategory
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Name { get; set; }

    [FileType(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.PictureType)]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Description { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public int ShowOrder { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Slug { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Keywords { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string MetaDescription { get; set; }

    public string CanonicalAddress { get; set; }
}
