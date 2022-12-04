namespace BlogMgmt.Application.Contract.ArticleCategory;

public record ArticleCategoryViewModel
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }
    public int ShowOrder { get; set; }
}
