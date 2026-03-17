using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels
{

public class Role : IdentityRole<int>
{
    public RoleValue RoleValue{get; set;}
    public Role() : base() {}
    public Role(string name, RoleValue roleValue) : base(name)
    {
        this.RoleValue= roleValue;
    }
}
}