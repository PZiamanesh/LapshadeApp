using _Framework.Application;
using DiscountMgmt.Application.Contract.ColleagueDiscount;
using DiscountMgmt.Domain.ColleagueDiscountAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiscountMgmt.Application;

public class ColleagueDiscountApplication : IColleagueDiscountApplication
{
    private readonly IColleagueDiscountRepository _colleagueDiscountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository, IUnitOfWork unitOfWork)
    {
        _colleagueDiscountRepository = colleagueDiscountRepository;
        _unitOfWork = unitOfWork;
    }

    public OperationResult Define(DefineColleagueDiscount command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        if (_colleagueDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var colleagueDiscount = new ColleagueDiscount(
            command.ProductId,
            command.DiscountRate
            );

        _colleagueDiscountRepository.Create(colleagueDiscount);
        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Edit(EditColleagueDiscount command)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);

        if (colleagueDiscount is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_colleagueDiscountRepository
            .Exists(x => x.ProductId == command.ProductId
                    && x.DiscountRate == command.DiscountRate
                    && x.Id != command.Id))
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.DuplicatedRecord);
        }

        colleagueDiscount.Edit(
            command.ProductId,
            command.DiscountRate
            );

        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public EditColleagueDiscount GetDetails(long id)
    {
        return _colleagueDiscountRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var colleagueDiscount = _colleagueDiscountRepository.Get(id);

        if (colleagueDiscount is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        colleagueDiscount.Remove();

        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var result = new OperationResult();
        _unitOfWork.BeginTrans();

        var colleagueDiscount = _colleagueDiscountRepository.Get(id);

        if (colleagueDiscount is null)
        {
            _unitOfWork.RollBack();
            return result.Failed(ApplicationMessage.RecordNotFound);
        }

        colleagueDiscount.Restore();

        _unitOfWork.Commit();
        return result.Succeeded();
    }

    public IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchViewModel search)
    {
        return _colleagueDiscountRepository.Search(search);
    }
}
