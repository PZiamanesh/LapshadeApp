using _Framework.Domain;
using DiscountMgmt.Application.Contract.ColleagueDiscount;

namespace DiscountMgmt.Domain.ColleagueDiscountAgg;

public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
{
    EditColleagueDiscount GetDetails(long id);
    IEnumerable<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchViewModel search);
}
