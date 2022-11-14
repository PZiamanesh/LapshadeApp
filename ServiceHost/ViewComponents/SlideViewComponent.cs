using _LampshadeQuery.Contract.Slide;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ServiceHost.ViewComponents;
#nullable disable

public class SlideViewComponent : ViewComponent
{
    private readonly ISlideQuery _slideQuery;

    public SlideViewComponent(ISlideQuery slideQuery)
    {
        _slideQuery = slideQuery;
    }

    public IViewComponentResult Invoke()
    {
        var slides = _slideQuery.GetSlides();
        return View(slides);
    }
}
