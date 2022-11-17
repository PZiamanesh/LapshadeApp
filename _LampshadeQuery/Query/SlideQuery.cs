using ShopMgmt.Infrastructure.EFCore;

namespace _LampshadeQuery.Contract.Slide;

public class SlideQuery : ISlideQuery
{
    private readonly ShopContext _dbContext;

    public SlideQuery(ShopContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<SlideQueryViewModel> GetSlides()
    {
        return _dbContext.Slides.Select(x => new SlideQueryViewModel
        {
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Heading = x.Heading,
            Title = x.Title,
            Text = x.Text,
            BtnText = x.BtnText,
            Link = x.Link
        }).ToList();
    }
}