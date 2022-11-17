namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record CustomerDiscountViewModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string Product { get; set; }
    public int DiscountRate { get; set; }
    public string Reason { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public DateTime StartDateEn { get; set; }
    public DateTime EndDateEn { get; set; }
}
