using _Framework.Domain;

namespace DiscountMgmt.Domain.CustomerDiscountAgg;

public interface ICustomerDiscountRepository:IRepository<long, CustomerDiscount>
{
}
