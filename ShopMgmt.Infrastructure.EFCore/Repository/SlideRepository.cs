using _Framework.Application;
using _Framework.Infrastructure;
using ShopMgmt.Application.Contract.Slide;
using ShopMgmt.Domain.SlideAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
{
    private readonly ShopContext _context;

    public SlideRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<SlideViewModel> GetAll()
    {
        return _context.Slides.Select(x => new SlideViewModel()
        {
            Id = x.Id,
            Picture = x.Picture,
            Heading = x.Heading,
            Title = x.Title,
            IsRemoved = x.IsRemoved,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToList();
    }

    public EditSlide GetDetails(long id)
    {
        return _context.Slides.Select(x => new EditSlide()
        {
            Id = x.Id,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Heading = x.Heading,
            Title = x.Title,
            Text = x.Text,
            BtnText = x.BtnText,
            Link = x.Link
        }).FirstOrDefault(x => x.Id == id);
    }
}
