namespace _LampshadeQuery.Contract.Slide;

public interface ISlideQuery
{
    IEnumerable<SlideQueryViewModel> GetSlides();
}
