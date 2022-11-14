using _Framework.Application;
using ShopMgmt.Application.Contract.Slide;
using ShopMgmt.Domain.SlideAgg;

namespace ShopMgmt.Application;
#nullable disable

public class SlideApplication : ISlideApplication
{
    private readonly ISlideRepository _slideRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SlideApplication(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
    {
        _slideRepository = slideRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Create(CreateSlide command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var slide = new Slide(command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Heading,
            command.Title,
            command.Text,
            command.BtnText,
            command.Link);

        _slideRepository.Create(slide);
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Edit(EditSlide command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var slide = _slideRepository.Get(command.Id);
        if (slide is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        slide.Edit(command.Picture,
            command.PictureAlt,
            command.PictureTitle,
            command.Heading,
            command.Title,
            command.Text,
            command.BtnText,
            command.Link);

        _unitOfWork.Commit();
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
        _unitOfWork.BeginTrans();

        var slide = _slideRepository.Get(id);
        if (slide is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        slide.Remove();
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var slide = _slideRepository.Get(id);
        if (slide is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        slide.Restore();
        _unitOfWork.Commit();
        return result.Succeeded();
    }
}
