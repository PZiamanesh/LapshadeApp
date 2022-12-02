namespace _LampshadeQuery.Contract.Slide;

public interface ISlideQuery
{
    IEnumerable<SlideQueryModel> GetSlides();
}
