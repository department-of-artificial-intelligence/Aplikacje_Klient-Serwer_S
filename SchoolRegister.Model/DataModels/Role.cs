using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Role : IdentityRole<int>
{
    RoleValue RoleValue { get; set; } = null!;
    Role(string name, RoleValue roleValue)
    {
        Name = name;
        RoleValue = roleValue;
    }

}