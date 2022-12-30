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

    public bool IsLoggedIn()
    {
        var claims = _contextAccessor.HttpContext.User.Claims;
        return claims.Any();
    }

    public void SignIn(AuthViewModel account)
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

        _contextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public void SignOut()
    {
        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}