using _Framework.Infrastructure;
using AccountMgmt.Application.Contract.Account;
using AccountMgmt.Domain.AccountAgg;

namespace AccountMgmt.Infrastructure.EFCore.Repository;

public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
{
    private readonly AccountContext _context;

    public AccountRepository(AccountContext context) : base(context)
    {
        _context = context;
    }

    public EditAccount GetDetails(long id)
    {
        return _context.Accounts
            .Select(x => new EditAccount
            {
                Id = id,
                Username = x.Username,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                RoleId = x.RoleId,

            }).FirstOrDefault(x => x.Id == id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        var query = _context.Accounts
            .Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Password = x.Password,
                Role = "مدیر سیستم",
                RoleId = x.RoleId,
                ProfilePhoto = x.ProfilePhoto,
            });

        if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
        {
            query = query.Where(x => x.Fullname == searchModel.Fullname);
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Username))
        {
            query = query.Where(x => x.Username == searchModel.Username);
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
        {
            query = query.Where(x => x.Mobile == searchModel.Mobile);
        }

        if (searchModel.RoleId > 0)
        {
            query = query.Where(x => x.RoleId == searchModel.RoleId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}
