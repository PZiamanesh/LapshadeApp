using _Framework.Application;

namespace ShopMgmt.Application.Contract.Slide;
#nullable disable

public interface ISlideApplication
{
    OperationResult Create(CreateSlide command);
    OperationResult Edit(EditSlide command);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditSlide GetDetails(long id);
    IEnumerable<SlideViewModel> GetAll();
}
