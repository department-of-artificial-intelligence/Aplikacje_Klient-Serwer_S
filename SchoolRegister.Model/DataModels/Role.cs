using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Role : IdentityRole<int>
{
    public RoleValue RoleValue { get; set; }
    public Role()
    {

    }
    public Role(string name, RoleValue roleValue)
    {
        Name = name;
        RoleValue = roleValue;
    }

}
