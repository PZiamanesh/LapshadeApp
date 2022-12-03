using System.Diagnostics.CodeAnalysis;

namespace _Framework.Domain;
#nullable disable

public class EntityBase<TKey>
{
    public TKey Id { get; private set; }
    public DateTime CreationDate { get; private set; }

    public EntityBase()
    {
        CreationDate = DateTime.Now;
    }
}
