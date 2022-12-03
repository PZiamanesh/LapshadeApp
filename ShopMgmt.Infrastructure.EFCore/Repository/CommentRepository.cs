using _Framework.Application;
using _Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Application.Contract.Comment;
using ShopMgmt.Domain.CommentAgg;

namespace ShopMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
{
    private readonly ShopContext _Context;

    public CommentRepository(ShopContext context) : base(context)
    {
        _Context = context;
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _Context.Comments
            .Include(x => x.Product)
            .Select(x => new CommentViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Product = x.Product.Name,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                CreationDate = x.CreationDate.ToFarsi(),
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled
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