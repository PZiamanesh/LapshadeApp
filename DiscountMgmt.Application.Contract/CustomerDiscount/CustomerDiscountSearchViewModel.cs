namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record CustomerDiscountSearchViewModel
{
    public long ProductId { get; set; }
    public string EndDate { get; set; }
    public string Reason { get; set; }
}