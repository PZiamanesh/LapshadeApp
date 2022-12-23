using _Framework.Domain;

namespace CommentMgmt.Domain.CommentAgg;
#nullable disable

public class Comment : EntityBase<long>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Message { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }
    public long OwnerRecordId { get; private set; }
    public int EntityType { get; private set; }

    public long ParentId { get; private set; }
    public Comment Parent { get; private set; }

    public List<Comment> Children { get; private set; }

    public Comment(string name, string email, string message,
        long ownerRecordId, int entityType, long parentId)
    {
        Name = name;
        Email = email;
        Message = message;
        IsConfirmed = false;
        IsCanceled = false;
        OwnerRecordId = ownerRecordId;
        EntityType = entityType;
        ParentId = parentId;
    }

    public void Confirm()
    {
        IsConfirmed = true;
        IsCanceled = false;
    }

    public void Cancel()
    {
        IsCanceled = true;
        IsConfirmed = false;
    }
}
