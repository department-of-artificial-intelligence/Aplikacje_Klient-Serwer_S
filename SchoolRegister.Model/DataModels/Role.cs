using Microsoft.AspNetCore.Identity;
namespace DataModels
{

    public class Role : IdentityRole<int>
    {
        public RoleValue RoleValue { get; set; }

        public Role() { }

        public Role(string name, RoleValue roleValue) : base(name)
        {
            RoleValue = roleValue;
        }
    }
}