using _Framework.Domain;

namespace AccountMgmt.Domain.RoleAgg;

public class Permission : EntityBase<long>
{
    public int Code { get; private set; }
    public string Name { get; private set; }
    public long RoleId { get; private set; }
    public Role Role { get; private set; }

    public Permission(int code, string name)
    {
        Code = code;
        Name = name;
    }
}