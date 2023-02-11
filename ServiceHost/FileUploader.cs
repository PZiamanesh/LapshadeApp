using _Framework.Application;

namespace ServiceHost;

public class FileUploader : IFileUploader
{
    string you = "done";
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Upload(IFormFile file, string subDirectory)
    {
        if (file is null)
        {
            return "";
        }

        var uniqueFileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";

        var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
            "ProductPictures",
            subDirectory,
            uniqueFileName);

        var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath,
            "ProductPictures",
            subDirectory);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using var outputFile = File.Create(filePath);
        await file.CopyToAsync(outputFile);
        return Path.Combine(subDirectory, uniqueFileName);
    }
}
