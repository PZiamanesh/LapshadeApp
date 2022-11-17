namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record DefineCustomerDiscount
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
}
