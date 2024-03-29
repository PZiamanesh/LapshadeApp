﻿namespace _Framework.Application;
#nullable disable

public class AuthenticationModel
{
    public long Id { get; private set; }
    public long RoleId { get; private set; }
    public string Username { get; private set; }
    public string Fullname { get; private set; }

    public AuthenticationModel(long id, long roleId, string username, string fullname)
    {
        Id = id;
        RoleId = roleId;
        Username = username;
        Fullname = fullname;
    }
}