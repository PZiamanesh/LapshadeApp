using _Framework.Application;
using _Framework.Infrastructure;
using AccountMgmt.Application.Contract.Role;
using AccountMgmt.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountMgmt.Infrastructure.EFCore.Repository;

public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
{
    private readonly AccountContext _context;

    public RoleRepository(AccountContext context) : base(context)
    {
        _context = context;
    }

    public EditRole GetDetails(long id)
    {
        return _context.Roles
            .Select(x => new EditRole
            {
                Id = id,
                Name = x.Name,
                
            }).FirstOrDefault(x => x.Id == id);
    }

    public List<RoleViewModel> GetRoles()
    {
        return _context.Roles
            .Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();
    }
}
