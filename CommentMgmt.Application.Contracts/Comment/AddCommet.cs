namespace CommentMgmt.Application.Contract.Comment;
#nullable disable

public record AddComment
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public long OwnerRecordId { get; set; }
    public int EntityType { get; set; }
    public long ParentId { get; set; }
}
