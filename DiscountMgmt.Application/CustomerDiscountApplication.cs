using _Framework.Application;
using DiscountMgmt.Application.Contract.CustomerDiscount;
using DiscountMgmt.Domain.CustomerDiscountAgg;

namespace DiscountMgmt.Application;

public class CustomerDiscountApplication : ICustomerDiscountApplication
{
    private readonly ICustomerDiscountRepository _customerDiscountRepository;

    public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
    {
        _customerDiscountRepository = customerDiscountRepository;
    }

    public OperationResult Define(DefineCustomerDiscount command)
    {
        var result = new OperationResult();

        if (_customerDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate))
        {
            
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var startDateEn = command.StartDate.ToGeorgianDateTime();
        var endDateEn = command.EndDate.ToGeorgianDateTime();

        var customerDiscount = new CustomerDiscount(
            command.ProductId,
            command.DiscountRate,
            startDateEn,
            endDateEn,
            command.Reason
            );

        _customerDiscountRepository.Create(customerDiscount);
        _customerDiscountRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Edit(EditCustomerDiscount command)
    {
        var result = new OperationResult();
        var customerDiscount = _customerDiscountRepository.Get(command.Id);

        if (customerDiscount is null)
        {
            
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_customerDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate
                    && x.Id != command.Id))
        {
            
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var startDateEn = command.StartDate.ToGeorgianDateTime();
        var endDateEn = command.EndDate.ToGeorgianDateTime();

        customerDiscount.Edit(
            command.ProductId,
            command.DiscountRate,
            startDateEn,
            endDateEn,
            command.Reason
            );

        _customerDiscountRepository.Save();
        return result.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditCustomerDiscount GetDetails(long id)
    {
        return _customerDiscountRepository.GetDetails(id);
    }

    public IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchViewModel search)
    {
        return _customerDiscountRepository.Search(search);
    }
}
