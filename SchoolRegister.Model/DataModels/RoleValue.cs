using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class RoleValue
{
    public int Id { get; set; }
    public string Value { get; set; }
    public Role Role { get; set; }
}