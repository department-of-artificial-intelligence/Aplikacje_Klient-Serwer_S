using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Role : IdentityRole<int>
{
    public RoleValue RoleValue { get; set; } = null!;
    public Role(string name, RoleValue roleValue) { }
}