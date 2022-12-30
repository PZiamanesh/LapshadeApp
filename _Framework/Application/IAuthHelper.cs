namespace _Framework.Application;

public interface IAuthHelper
{
    void SignIn(AuthViewModel authModel);
    void SignOut();
    bool IsLoggedIn();
}
