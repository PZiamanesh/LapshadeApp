using System.Diagnostics.CodeAnalysis;

namespace _Framework.Domain;

public class EntityBase<TKey>
{
    public TKey Id { get; private set; } = default!;
    public DateTime CreationDate { get; private set; }

    public EntityBase()
    {
        CreationDate = DateTime.Now;
    }
}
