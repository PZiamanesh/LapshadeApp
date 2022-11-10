using _Framework.Application;

namespace ShopMgmt.Infrastructure.EFCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly LampShadeDbContext _context;

    public UnitOfWork(LampShadeDbContext context)
    {
        _context = context;
    }

    public void BeginTrans()
    {
        _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        _context.Database.CommitTransaction();
    }

    public void RollBack()
    {
        _context.Database.RollbackTransaction();
    }
}
