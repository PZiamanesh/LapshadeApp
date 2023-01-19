using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _Framework.Application;
#nullable disable

public class AuthHelper : IAuthHelper
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public bool IsAuthonticated()
    {
        var claims = _contextAccessor.HttpContext.User.Claims;
        return claims.Any();
    }

    public string AuthorizedRole()
    {
        if (IsAuthonticated())
        {
            return _contextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        }
        else
        {
            return null;
        }
    }

    public void SignIn(AuthenticationModel account)
    {
        var claims = new List<Claim>
        {
            new Claim("AccountId", account.Id.ToString()),
            new Claim(ClaimTypes.Name, account.Fullname),
            new Claim(ClaimTypes.Role, account.RoleId.ToString()),
            new Claim("Username", account.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
        };

        // handle signIn
        _contextAccessor.HttpContext
            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public void SignOut()
    {
        // handle signout
        _contextAccessor.HttpContext
            .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public UserInfoModel UserInfo()
    {
        var user = new UserInfoModel();

        if (!IsAuthonticated())
        {
            return user;
        }

        var claims = _contextAccessor.HttpContext.User.Claims.ToList();
        user.UserName = claims.FirstOrDefault(x => x.Type == "Username").Value;
        user.RoleId = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        user.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);

        return user;
    }
}