using System;
using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels
{

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public User() { }
    }
}