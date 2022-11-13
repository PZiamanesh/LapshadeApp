using _Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Application.Contract.Slide;
using ShopMgmt.Domain.SlideAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class SlideRepository : BaseRepository<long, Slide>, ISlideRepository
{
    private readonly LampShadeDbContext _context;

    public SlideRepository(LampShadeDbContext context) : base(context)
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
            Title = x.Title
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
            BtnText = x.BtnText
        }).FirstOrDefault(x => x.Id == id);
    }
}
