using _Framework.Domain;
using CommentMgmt.Application.Contract.Comment;

namespace CommentMgmt.Domain.CommentAgg;
#nullable disable

public interface ICommentRepository : IRepository<long, Comment>
{
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}
