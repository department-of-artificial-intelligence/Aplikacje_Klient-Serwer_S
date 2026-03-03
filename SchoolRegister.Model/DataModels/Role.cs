public class Role
{
    public int RoleValue { get; set; }
    public string RoleName { get; set; }

    public Role(int roleValue, string roleName)
    {
        RoleValue = roleValue;
        RoleName = roleName;
    }
}