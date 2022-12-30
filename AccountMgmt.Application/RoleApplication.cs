using _Framework.Application;
using AccountMgmt.Application.Contract.Role;
using AccountMgmt.Domain.RoleAgg;

namespace AccountMgmt.Application;

public class RoleApplication : IRoleApplication
{
    private readonly IRoleRepository _roleRepository;

    public RoleApplication(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public OperationResult Create(CreateRole command)
    {
        var taskResult = new OperationResult();

        if (_roleRepository.Exists(x=> x.Name == command.Name))
        {
            return taskResult.Failed(ApplicationMessage.DuplicatedRecord);
        }

        var role = new Role(command.Name);
        _roleRepository.Create(role);
        _roleRepository.Save();
        return taskResult.Succeeded("نقش با موفقیت ایجاد شد");
    }

    public OperationResult Edit(EditRole command)
    {
        var taskResult = new OperationResult();
        var role = _roleRepository.Get(command.Id);

        if (role is null)
            return taskResult.Failed(ApplicationMessage.RecordNotFound);

        if (_roleRepository.Exists(x=> x.Name == command.Name && x.Id != command.Id))
        {
            return taskResult.Failed(ApplicationMessage.DuplicatedRecord);
        }

        role.Edit(command.Name);
        _roleRepository.Save();
        return taskResult.Succeeded(ApplicationMessage.RecordEdited);
    }

    public EditRole GetDetails(long id)
    {
        return _roleRepository.GetDetails(id);
    }

    public List<RoleViewModel> GetRoles()
    {
        return _roleRepository.GetRoles();
    }
}
