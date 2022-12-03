using _Framework.Application;
using ShopMgmt.Application.Contract.Comment;
using ShopMgmt.Domain.CommentAgg;

namespace ShopMgmt.Application;
#nullable disable

public class CommentApplication : ICommentApplication
{
    private readonly ICommentRepository _commentRepository;

    public CommentApplication(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public OperationResult AddComment(AddComment command)
    {
        var result = new OperationResult();
        var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);
        _commentRepository.Create(comment);
        _commentRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Cancel(long id)
    {
        var result = new OperationResult();
        var comment = _commentRepository.Get(id);

        if (comment is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        comment.Cancel();
        _commentRepository.Save();
        return result.Succeeded();
    }
    public OperationResult Confirm(long id)
    {
        var result = new OperationResult();
        var comment = _commentRepository.Get(id);

        if (comment is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        comment.Confirm();
        _commentRepository.Save();
        return result.Succeeded();
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        return _commentRepository.Search(searchModel);
    }
}
