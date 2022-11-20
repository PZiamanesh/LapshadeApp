using _Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _Framework.Infrastructure;

public class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : EntityBase<TKey>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _entities;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public void Create(TEntity item)
    {
        _entities.Add(item);
    }

    public TEntity? Get(TKey id)
    {
        return _entities.Find(id);
    }

    public IEnumerable<TEntity> Get()
    {
        return _entities.ToList();
    }

    public bool Exists(Func<TEntity, bool> expression)
    {
        return _entities.Any(expression);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}