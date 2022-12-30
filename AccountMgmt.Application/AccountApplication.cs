using _Framework.Application;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Domain.AccountAgg;

namespace AccountMgmt.Application;

public class AccountApplication : IAccountApplication
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IFileUploader _fileUploader;
    private readonly IAuthHelper _authHelper;

    public AccountApplication(
        IAccountRepository accountRepository,
        IPasswordHasher passwordHasher,
        IFileUploader fileUploader,
        IAuthHelper authHelper)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _fileUploader = fileUploader;
        _authHelper = authHelper;
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
            return taskResult.Failed(ApplicationMessage.PasswordNotMatched);
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

    public OperationResult Login(Login command)
    {
        var result = new OperationResult();
        var account = _accountRepository.GetByUser(command.Username);

        if (account is null)
        {
           return result.Failed(ApplicationMessage.WrongLoginInfo);
        }

        var passwordResult = _passwordHasher.Check(account.Password, command.Password);

        if (!passwordResult.Verified)
        {
           return result.Failed(ApplicationMessage.WrongLoginInfo);
        }

        var authModel = new AuthViewModel(account.Id, account.RoleId, account.Username, account.Fullname);

        _authHelper.SignIn(authModel);

        return result.Succeeded("کاربر با موفقیت وارد سیستم شد");
    }

    public void Logout()
    {
        _authHelper.SignOut();
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }
}
