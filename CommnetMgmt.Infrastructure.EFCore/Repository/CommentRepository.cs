using _Framework.Application;
using _Framework.Infrastructure;
using CommentMgmt.Application.Contract.Comment;
using CommentMgmt.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
{
    private readonly CommentContext _Context;

    public CommentRepository(CommentContext context) : base(context)
    {
        _Context = context;
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _Context.Comments
            .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                CreationDate = x.CreationDate.ToFarsi(),
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled,
                OwnerRecordId = x.OwnerRecordId,
                EntityType = x.EntityType
            }).AsNoTracking().OrderByDescending(x => x.Id).ToList();


        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name.Contains(searchModel.Name)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Email))
        {
            query = query.Where(x => x.Name.Contains(searchModel.Email)).ToList();
        }

        return query;
    }
}