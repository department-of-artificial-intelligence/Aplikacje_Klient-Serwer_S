using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class Role : IdentitRole<int>{
    public RoleValue RoleValue {get;set;}
    public Role() : base(){
        RoleValue = RoleValue.User;
    }
    public Role(string name,RoleValue roleValue) : base(name){
        RoleValue = roleValue;
    }
}