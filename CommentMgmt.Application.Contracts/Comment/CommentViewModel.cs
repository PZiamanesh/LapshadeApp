namespace CommentMgmt.Application.Contract.Comment;
#nullable disable

public record CommentViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public string CreationDate { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsCanceled { get; set; }
    public string OwnerName { get; set; }
    public long OwnerRecordId { get; set; }
    public int EntityType { get; set; }
}
