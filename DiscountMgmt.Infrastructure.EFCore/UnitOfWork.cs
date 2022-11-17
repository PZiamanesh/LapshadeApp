using _Framework.Application;

namespace DiscountMgmt.Infrastructure.EFCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly DiscountContext _context;

    public UnitOfWork(DiscountContext context)
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
