﻿namespace AccountMgmt.Application.Contract.Account;

public class AccountViewModel
{
    public long Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Mobile { get; set; }
    public string Role { get; set; }
    public long RoleId { get; set; }
    public string ProfilePhoto { get; set; }
    public string CreationDate { get; set; }
}
