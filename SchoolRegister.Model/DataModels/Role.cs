using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
namespace SchoolRegister.Model.DataModels;

public class Role : IdentityRole<int>
{
    public RoleValue RoleValue { get; set; }
    public Role(string name, RoleValue rolevalue)
    {
        RoleValue = rolevalue;
        Name = name;
    }
}
