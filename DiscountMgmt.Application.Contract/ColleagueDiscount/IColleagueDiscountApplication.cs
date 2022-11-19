using _Framework.Application;

namespace DiscountMgmt.Application.Contract.ColleagueDiscount;

public interface IColleagueDiscountApplication
{
    OperationResult Define(DefineColleagueDiscount command);
    OperationResult Edit(EditColleagueDiscount command);
    EditColleagueDiscount GetDetails(long id);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchViewModel search);
}
