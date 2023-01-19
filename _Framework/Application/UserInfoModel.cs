using _Framework.Infrastructure;

namespace _Framework.Application;
#nullable disable

public class UserInfoModel
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string RoleId { get; set; }
    public string ProfilePicture { get; set; }

    public string RoleMean()
    {
        return RoleId switch
        {
            "1" => nameof(Roles.Admin),
            "2" => nameof(Roles.SiteUser),
            "3" => nameof(Roles.ContentLoader),
            _=> ""
        };
    }
}
