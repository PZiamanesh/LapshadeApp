using System.Linq.Expressions;

namespace _Framework.Domain;

public interface IRepository<in TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    void Create(TEntity item);

    TEntity? Get(TKey id);

    IEnumerable<TEntity> Get();

    bool Exists(Expression<Func<TEntity, bool>> expression);
}