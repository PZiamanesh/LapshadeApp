namespace ShopMgmt.Application.Contract.Comment;
#nullable disable

public record AddComment
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
