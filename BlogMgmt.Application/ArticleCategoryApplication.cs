using _Framework.Application;
using BlogMgmt.Application.Contract.ArticleCategory;
using BlogMgmt.Domain.ArticleCategoryAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogMgmt.Application;

public class ArticleCategoryApplication : IArticleCategoryApplication
{
    private readonly IArticleCategoryRepository _articleCategoryRepository;
    private readonly IFileUploader _fileUploader;

    public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository,
        IFileUploader fileUploader)
    {
        _articleCategoryRepository = articleCategoryRepository;
        _fileUploader = fileUploader;
    }

    public async Task<OperationResult> Create(CreateArticleCategory command)
    {
        var result = new OperationResult();

        if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug.Slugify();
        var articleDirectoryPath = Path.Combine("Articles", slug);
        var picturePath = await _fileUploader.Upload(command.Picture, articleDirectoryPath);

        var articleCategory = new ArticleCategory(
            command.Name,
            picturePath,
            command.PictureAlt,
            command.PictureTitle,
            command.ShowOrder,
            slug,
            command.Keywords,
            command.MetaDescription,
            command.CanonicalAddress,
            command.Description
            );

        _articleCategoryRepository.Create(articleCategory);
        _articleCategoryRepository.Save();
        return result.Succeeded();
    }

    public async Task<OperationResult> Edit(EditArticleCategory command)
    {
        var result = new OperationResult();
        var articleCategory = _articleCategoryRepository.Get(command.Id);

        if (articleCategory == null)
            return result.Failed(ApplicationMessage.RecordNotFound);

        if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var slug = command.Slug.Slugify();
        var articleDirectoryPath = Path.Combine("Articles", slug);
        var picturePath = await _fileUploader.Upload(command.Picture, articleDirectoryPath);

        articleCategory.Edit(
           command.Name,
           picturePath,
           command.PictureAlt,
           command.PictureTitle,
           command.ShowOrder,
           slug,
           command.Keywords,
           command.MetaDescription,
           command.CanonicalAddress,
           command.Description
           );

        _articleCategoryRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditArticleCategory GetDetails(long id)
    {
        return _articleCategoryRepository.GetDetails(id);
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
    {
        return _articleCategoryRepository.Search(searchModel);
    }
}
