using _Framework.Application;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Domain.AccountAgg;

namespace AccountMgmt.Application;

public class AccountApplication : IAccountApplication
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IFileUploader _fileUploader;

    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
        IFileUploader fileUploader)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _fileUploader = fileUploader;
    }

    public OperationResult ChangePassword(ChangePassword command)
    {
        var taskResult = new OperationResult();
        var userAccount = _accountRepository.Get(command.Id);

        if (userAccount is null)
        {
            return taskResult.Failed(ApplicationMessage.RecordNotFound);
        }

        if (command.Password != command.RePassword)
        {
            return taskResult.Failed(ApplicationMessage.PasswordNotMatcg);
        }

        string hashedPassword = _passwordHasher.Hash(command.Password);
        userAccount.ChangePassword(hashedPassword);
        _accountRepository.Save();
        return taskResult.Succeeded();
    }

    public async Task<OperationResult> Create(CreateAccount command)
    {
        var taskResult = new OperationResult();

        if ( _accountRepository.Exists(x=> 
                x.Username.Equals(command.Username, StringComparison.InvariantCultureIgnoreCase) ||
                x.Mobile.Equals(command.Mobile, StringComparison.InvariantCultureIgnoreCase))
           )
        {
            return taskResult.Failed("کاربری با این مشخصات ثبت نام کرده است");
        }

        var hashedPassword = _passwordHasher.Hash(command.Password);
        var profilePicture = await _fileUploader.Upload(command.ProfilePhoto, "profilePicture");

        var newAccount = new Account(command.Fullname, command.Username,
            hashedPassword, command.Mobile, command.RoleId, profilePicture);

        _accountRepository.Create(newAccount);
        _accountRepository.Save();
        return taskResult.Succeeded();
    }

    public async Task<OperationResult> Edit(EditAccount command)
    {
        var taskResult = new OperationResult();
        var account = _accountRepository.Get(command.Id);

        if (_accountRepository.Exists(
                x =>
                    (x.Username.Equals(command.Username, StringComparison.InvariantCultureIgnoreCase) ||
                     x.Mobile.Equals(command.Mobile, StringComparison.InvariantCultureIgnoreCase)) &&
                     x.Id != command.Id
              )
           )
        {
            return taskResult.Failed("کاربری با این مشخصات وجود دارد است");
        }

        var picture = await _fileUploader.Upload(command.ProfilePhoto, "profilePicture");

        account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picture);
        _accountRepository.Save();
        return taskResult.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditAccount GetDetails(long id)
    {
        return _accountRepository.GetDetails(id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }
}
