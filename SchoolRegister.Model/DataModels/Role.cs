using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
namespace SchoolRegister.Model.DataModels;

public class Role : IdentityRole<int>
{
public RoleValue RoleValue { get; set; }
public Role() : base()
{
RoleValue = RoleValue.User;
}
public Role(string name, RoleValue roleValue) : base(name)
{
RoleValue = roleValue;
}
}