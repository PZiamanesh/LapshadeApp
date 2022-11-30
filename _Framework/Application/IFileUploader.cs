using Microsoft.AspNetCore.Http;

namespace _Framework.Application;

public interface IFileUploader
{
    Task<string> Upload(IFormFile file, string subDirectory);
}
