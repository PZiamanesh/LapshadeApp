namespace DiscountMgmt.Application.Contract.ColleagueDiscount;

public record DefineColleagueDiscount
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}
