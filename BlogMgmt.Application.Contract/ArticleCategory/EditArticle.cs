namespace BlogMgmt.Application.Contract.ArticleCategory;

public record EditArticle : CreateArticle
{
    public long Id { get; set; }
}
