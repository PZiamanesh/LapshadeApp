using System.Diagnostics.CodeAnalysis;

namespace _Framework.Domain;

public class BaseEntity<TKey>
{
    public TKey Id { get; private set; } = default!;
    public DateTime CreationDate { get; private set; }

    public BaseEntity()
    {
        CreationDate = DateTime.Now;
    }
}
