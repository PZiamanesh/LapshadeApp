using _Framework.Domain;
using BlogMgmt.Domain.ArticleAgg;

namespace BlogMgmt.Domain.ArticleCategoryAgg;

public class ArticleCategory : EntityBase<long>
{
    public string Name { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public string Description { get; private set; }
    public int ShowOrder { get; private set; }
    public string Slug { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }
    public string CanonicalAddress { get; private set; }

    public List<Article> Articles { get; private set; }

    protected ArticleCategory()
    {
        Articles = new List<Article>();
    }

    public ArticleCategory(
        string name,
        string picture,
        string pictureAlt,
        string pictureTitle,
        int showOrder,
        string slug,
        string keywords,
        string metaDescription,
        string canonicalAddress,
        string description)
    {
        Name = name;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        ShowOrder = showOrder;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        Description = description;
    }

    public void Edit(
        string name,
        string picturePath,
        string pictureAlt,
        string pictureTitle,
        int showOrder,
        string slug,
        string keywords,
        string metaDescription,
        string canonicalAddress,
        string description)
    {
        Name = name;

        if (!string.IsNullOrWhiteSpace(picturePath))
        {
            Picture = picturePath;
        }

        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        ShowOrder = showOrder;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        Description = description;
    }
}
