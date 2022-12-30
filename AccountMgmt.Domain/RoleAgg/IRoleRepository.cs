using _Framework.Domain;
using AccountMgmt.Application.Contract.Role;

namespace AccountMgmt.Domain.RoleAgg;

public interface IRoleRepository : IRepository<long,Role>
{
    EditRole GetDetails(long id);
    List<RoleViewModel> GetRoles();
}
