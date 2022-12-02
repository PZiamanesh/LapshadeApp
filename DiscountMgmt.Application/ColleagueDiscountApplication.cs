using _Framework.Application;
using DiscountMgmt.Application.Contract.ColleagueDiscount;
using DiscountMgmt.Domain.ColleagueDiscountAgg;

namespace DiscountMgmt.Application;

public class ColleagueDiscountApplication : IColleagueDiscountApplication
{
    private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

    public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
    {
        _colleagueDiscountRepository = colleagueDiscountRepository;
    }

    public OperationResult Define(DefineColleagueDiscount command)
    {
        var result = new OperationResult();

        if (_colleagueDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var colleagueDiscount = new ColleagueDiscount(
            command.ProductId,
            command.DiscountRate
            );

        _colleagueDiscountRepository.Create(colleagueDiscount);
        _colleagueDiscountRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Edit(EditColleagueDiscount command)
    {
        var result = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);

        if (colleagueDiscount is null)
        {

            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_colleagueDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate
                    && x.Id != command.Id))
        {
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        colleagueDiscount.Edit(
            command.ProductId,
            command.DiscountRate
            );

        _colleagueDiscountRepository.Save();
        return result.Succeeded();
    }

    public EditColleagueDiscount GetDetails(long id)
    {
        return _colleagueDiscountRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var result = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.Get(id);

        if (colleagueDiscount is null)
        {

            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        colleagueDiscount.Remove();

        _colleagueDiscountRepository.Save();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        var colleagueDiscount = _colleagueDiscountRepository.Get(id);

        if (colleagueDiscount is null)
        {
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        colleagueDiscount.Restore();

        _colleagueDiscountRepository.Save();
        return result.Succeeded();
    }

    public IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel search)
    {
        return _colleagueDiscountRepository.Search(search);
    }
}
