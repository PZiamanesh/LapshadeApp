namespace ShopMgmt.Application.Contract.Comment;
#nullable disable

public record CommentSearchModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}