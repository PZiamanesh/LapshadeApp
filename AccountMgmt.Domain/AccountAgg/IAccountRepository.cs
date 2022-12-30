using _Framework.Domain;
using AccountMgmt.Application.Contract.Account;

namespace AccountMgmt.Domain.AccountAgg;

public interface IAccountRepository : IRepository<long, Account>
{
    Account GetByUser(string username);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
}
