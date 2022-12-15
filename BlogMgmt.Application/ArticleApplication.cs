using _Framework.Application;
using BlogMgmt.Application.Contract.Article;
using BlogMgmt.Domain.ArticleAgg;
using BlogMgmt.Domain.ArticleCategoryAgg;

namespace BlogMgmt.Application;

public class ArticleApplication : IArticleApplication
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleCategoryRepository _articleCategoryRepository;
    private readonly IFileUploader _fileUploader;

    public ArticleApplication(IArticleRepository articleRepository,
        IArticleCategoryRepository articleCategoryRepository,
        IFileUploader fileUploader)
    {
        _articleRepository = articleRepository;
        _articleCategoryRepository = articleCategoryRepository;
        _fileUploader = fileUploader;
    }

    public async Task<OperationResult> Create(CreateArticle command)
    {
        var result = new OperationResult();

        if (_articleRepository.Exists(x => x.Title == command.Title))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var articleSlug = command.Slug.Slugify();
        var articleCategorySlug = _articleCategoryRepository.GetSlug(command.CategoryId);
        var articleFolderPath = Path.Combine("Articles", articleCategorySlug, articleSlug);
        var picturePath = await _fileUploader.Upload(command.Picture, articleFolderPath);
        var publishDate = command.PublishDate.ToGeorgianDateTime();

        var article = new Article(
            command.Title,
            command.ShortDescription,
            command.Description,
            publishDate,
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            articleSlug,
            command.Keywords,
            command.MetaDescription,
            command.CanonicalAddress,
            command.CategoryId
            );

        _articleRepository.Create(article);
        _articleRepository.Save();
        return result.Succeeded();
    }

    public async Task<OperationResult> Edit(EditArticle command)
    {
        var result = new OperationResult();
        var article = _articleRepository.GetWithDescendants(command.Id);

        if (article is null)
            return result.Failed(ApplicationMessage.RecordNotFound);

        if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            return result.Failed(ApplicationMessage.DuplicatedRecord);

        var articleSlug = command.Slug.Slugify();
        var articleFolderPath = Path.Combine("Articles", article.Category.Slug, articleSlug);
        var picturePath = await _fileUploader.Upload(command.Picture, articleFolderPath);
        var publishDate = command.PublishDate.ToGeorgianDateTime();

        article.Edit(
             command.Title,
             command.ShortDescription,
             command.Description,
             publishDate,
             picturePath,
             command.PictureAlt,
             command.PictureTitle,
             articleSlug,
             command.Keywords,
             command.MetaDescription,
             command.CanonicalAddress,
             command.CategoryId
             );

        _articleRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditArticle GetDetails(long id)
    {
        return _articleRepository.GetDetails(id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        return _articleRepository.Search(searchModel);
    }
}
