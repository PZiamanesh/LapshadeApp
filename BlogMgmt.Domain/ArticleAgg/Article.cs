using _Framework.Domain;
using BlogMgmt.Domain.ArticleCategoryAgg;

namespace BlogMgmt.Domain.ArticleAgg;

public class Article : EntityBase<long>
{
    public string Title { get; private set; }
    public string ShortDescription { get; private set; }
    public string Description { get; private set; }
    public DateTime PublishDate { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public string Slug { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }
    public string CanonicalAddress { get; private set; }

    public long CategoryId { get; private set; }
    public ArticleCategory Category { get; private set; }

    public Article(string title, string shortDescription, string description,
        DateTime publishDate, string picture, string pictureAlt,
        string pictureTitle, string slug, string keywords,
        string metaDescription, string canonicalAddress, long categoryId)
    {
        Title = title;
        ShortDescription = shortDescription;
        Description = description;
        PublishDate = publishDate;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        CategoryId = categoryId;
    }

    public void Edit(string title, string shortDescription, string description,
        DateTime publishDate, string picture, string pictureAlt,
        string pictureTitle, string slug, string keywords,
        string metaDescription, string canonicalAddress, long categoryId)
    {
        Title = title;
        ShortDescription = shortDescription;
        Description = description;
        PublishDate = publishDate;

        if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;

        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        CategoryId = categoryId;
    }
}
