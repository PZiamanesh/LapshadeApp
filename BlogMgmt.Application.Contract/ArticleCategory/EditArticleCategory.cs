namespace BlogMgmt.Application.Contract.ArticleCategory;

public record EditArticleCategory : CreateArticleCategory
{
    public long Id { get; set; }
}
