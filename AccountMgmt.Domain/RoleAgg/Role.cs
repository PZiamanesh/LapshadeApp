using _Framework.Domain;
using AccountMgmt.Domain.AccountAgg;

namespace AccountMgmt.Domain.RoleAgg;

public class Role : EntityBase<long>
{
    public string Name { get; private set; }
    public List<Account> Accounts { get; set; }
    public List<Permission> Permissions { get; private set; }

    protected Role() { }

    public Role(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public void Edit(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}
