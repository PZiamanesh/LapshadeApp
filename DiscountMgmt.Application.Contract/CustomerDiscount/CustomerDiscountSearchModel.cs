namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record CustomerDiscountSearchModel
{
    public long ProductId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}