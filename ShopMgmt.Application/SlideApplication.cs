using _Framework.Application;
using ShopMgmt.Application.Contract.Slide;
using ShopMgmt.Domain.SlideAgg;

namespace ShopMgmt.Application;
#nullable disable

public class SlideApplication : ISlideApplication
{
    private readonly ISlideRepository _slideRepository;
    private readonly IFileUploader _fileUploader;

    public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
    {
        _slideRepository = slideRepository;
        _fileUploader = fileUploader;
    }

    public async Task<OperationResult> Create(CreateSlide command)
    {
        var result = new OperationResult();
        var picturePath = await _fileUploader.Upload(command.Picture, "slides");

        var slide = new Slide(
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            command.Heading,
            command.Title,
            command.Text,
            command.BtnText,
            command.Link);

        _slideRepository.Create(slide);
        _slideRepository.Save();
        return result.Succeeded();
    }

    public async Task<OperationResult> Edit(EditSlide command)
    {
        var result = new OperationResult();
        var slide = _slideRepository.Get(command.Id);

        if (slide is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        var picturePath = await _fileUploader.Upload(command.Picture, "slides");

        slide.Edit(
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            command.Heading,
            command.Title,
            command.Text,
            command.BtnText,
            command.Link);

        _slideRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public IEnumerable<SlideViewModel> GetAll()
    {
        return _slideRepository.GetAll();
    }

    public EditSlide GetDetails(long id)
    {
        return _slideRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var result = new OperationResult();
        var slide = _slideRepository.Get(id);

        if (slide is null)
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        slide.Remove();
        _slideRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        var slide = _slideRepository.Get(id);

        if (slide is null)
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        slide.Restore();
        _slideRepository.Save();
        return result.Succeeded();
    }
}
