namespace _Framework.Application;

public interface IAuthHelper
{
    void SignIn(AuthenticationModel authModel);
    void SignOut();
    bool IsAuthonticated();
    string AuthorizedRole();
    UserInfoModel UserInfo();
}
