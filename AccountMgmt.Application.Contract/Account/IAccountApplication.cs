using _Framework.Application;

namespace AccountMgmt.Application.Contract.Account;

public interface IAccountApplication
{
    Task<OperationResult> Create(CreateAccount command);
    Task<OperationResult> Edit(EditAccount command);
    OperationResult ChangePassword(ChangePassword command);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
}