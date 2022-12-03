using _Framework.Application;

namespace ShopMgmt.Application.Contract.Comment;

public interface ICommentApplication
{
    OperationResult AddComment(AddComment command);
    OperationResult Cancel(long id);
    OperationResult Confirm(long id);
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}
