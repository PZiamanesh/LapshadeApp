﻿using _Framework.Application;

namespace ShopMgmt.Infrastructure.EFCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopContext _context;

    public UnitOfWork(ShopContext context)
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
