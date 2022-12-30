using _Framework.Application;

namespace AccountMgmt.Application.Contract.Role;

public interface IRoleApplication
{
    OperationResult Create(CreateRole command);
    OperationResult Edit(EditRole command);
    EditRole GetDetails(long id);
    List<RoleViewModel> GetRoles();
}
