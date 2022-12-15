namespace _LampshadeQuery.Contract.Article;

public interface IArticleQuery
{
    List<ArticleQueryModel> GetLatestArticles();
}
