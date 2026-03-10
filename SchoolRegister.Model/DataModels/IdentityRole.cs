namespace SchoolRegister.Model.DataModels
{
    public enum RoleValue
    {
        User = 0,
        Student = 1,
        Parent = 2,
        Teacher = 3,
        Admin = 4
    }

    public class Role
    {
        public int Id { get; set; } // IdentityRole Id
        public RoleValue RoleValue { get; set; }

        public Role() { }

        public Role(string name, RoleValue roleValue)
        {
            RoleValue = roleValue;
        }
    }
}