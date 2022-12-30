using _Framework.Domain;
using AccountMgmt.Domain.AccountAgg;

namespace AccountMgmt.Domain.RoleAgg;

public class Role : EntityBase<long>
{
    public string Name { get; private set; }
    public List<Account> Accounts { get; set; }

    public Role(string name)
    {
        Name = name;
    }

    public void Edit(string name)
    {
        Name = name;
    }
}
