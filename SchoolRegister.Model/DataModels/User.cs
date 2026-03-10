using Microsoft.AspNetCore.Identity;
using System;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegistrationDate { get; set; }

    public User() { }
}