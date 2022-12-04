using _Framework.Domain;
using DiscountMgmt.Application.Contract.CustomerDiscount;

namespace DiscountMgmt.Domain.CustomerDiscountAgg;

public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
{
    EditCustomerDiscount GetDetails(long id);
    IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);
}
