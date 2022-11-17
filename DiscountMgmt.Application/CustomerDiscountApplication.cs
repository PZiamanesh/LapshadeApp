using _Framework.Application;
using DiscountMgmt.Application.Contract.CustomerDiscount;
using DiscountMgmt.Domain.CustomerDiscountAgg;

namespace DiscountMgmt.Application;

public class CustomerDiscountApplication : ICustomerDiscountApplication
{
    private readonly ICustomerDiscountRepository _customerDiscountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository, IUnitOfWork unitOfWork)
    {
        _customerDiscountRepository = customerDiscountRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Define(DefineCustomerDiscount command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        if (_customerDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate))
        {
            _unitOfWork.RollBack();
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
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Edit(EditCustomerDiscount command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var customerDiscount = _customerDiscountRepository.Get(command.Id);

        if (customerDiscount is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_customerDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate
                    && x.Id != command.Id))
        {
            _unitOfWork.RollBack();
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

        _unitOfWork.Commit();
        return result.Succeeded();
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
