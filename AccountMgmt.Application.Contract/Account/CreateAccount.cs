using _Framework.Application;
using AccountMgmt.Application.Contract.Role;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountMgmt.Application.Contract.Account;

public class CreateAccount
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Fullname { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Username { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Password { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string RePassword { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Mobile { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [Range(1, byte.MaxValue)]
    public long RoleId { get; set; }

    public IFormFile ProfilePhoto { get; set; }

    public List<RoleViewModel> Roles { get; set; }
}
