using _Framework.Domain;
using ShopMgmt.Application.Contract.Slide;

namespace ShopMgmt.Domain.SlideAgg;
#nullable disable

public interface ISlideRepository : IRepository<long, Slide>
{
    IEnumerable<SlideViewModel> GetAll();
    EditSlide GetDetails(long id);
}
