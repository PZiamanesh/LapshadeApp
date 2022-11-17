namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public record EditCustomerDiscount : DefineCustomerDiscount
{
    public long Id { get; set; }
}
