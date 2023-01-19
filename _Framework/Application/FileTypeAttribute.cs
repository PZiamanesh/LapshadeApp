using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace _Framework.Application;

public class FileTypeAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string[] _fileTypes;

    public FileTypeAttribute(string[] fileTypes)
    {
        _fileTypes = fileTypes;
    }

    public override bool IsValid(object? value)
    {
        var file = value as IFormFile;
        if (file == null) return true;

        string fileType = Path.GetExtension(file.FileName);
        return _fileTypes.Contains(fileType);
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.Add("data-val-fileType", ErrorMessage);
    }
}
