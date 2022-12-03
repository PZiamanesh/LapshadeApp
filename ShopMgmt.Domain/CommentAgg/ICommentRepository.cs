using _Framework.Domain;
using ShopMgmt.Application.Contract.Comment;

namespace ShopMgmt.Domain.CommentAgg;
#nullable disable

public interface ICommentRepository : IRepository<long, Comment>
{
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}
