using _Framework.Application;

namespace DiscountMgmt.Application.Contract.CustomerDiscount;

public interface ICustomerDiscountApplication
{
    OperationResult Define(DefineCustomerDiscount command);
    OperationResult Edit(EditCustomerDiscount command);
    EditCustomerDiscount GetDetails(long id);
    IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);
}
