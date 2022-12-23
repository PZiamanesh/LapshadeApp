using CommentMgmt.Application;
using CommentMgmt.Application.Contract.Comment;
using CommentMgmt.Domain.CommentAgg;
using CommentMgmt.Infrastructure.EFCore;
using CommentMgmt.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentMgmt.Infrastructure.Configuration;

public class CommentMgmtBootstrapper
{
    public static void ConfigureService(IServiceCollection service, string connectionString)
    {
        service.AddScoped<ICommentApplication, CommentApplication>();
        service.AddScoped<ICommentRepository, CommentRepository>();

        service.AddDbContext<CommentContext>(opt => opt.UseSqlServer(connectionString));
    }
}