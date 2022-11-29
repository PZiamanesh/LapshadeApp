using _Framework.Application;

namespace ServiceHost;

public class FileUploader : IFileUploader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Upload(IFormFile file, string slugName)
    {
        if (file is null)
        {
            return "";
        }

        var uniqueFileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";

        var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
            "ProductPictures",
            slugName,
            uniqueFileName);

        var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath,
            "ProductPictures",
            slugName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using var outputFile = File.Create(filePath);
        await file.CopyToAsync(outputFile);
        return Path.Combine(slugName, uniqueFileName);
    }
}
