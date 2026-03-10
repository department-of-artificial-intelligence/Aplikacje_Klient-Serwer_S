using System;

namespace SchoolRegister.Model.DataModels
{
    public class IdentityUser
    {
        public int Id { get; set; }
    }

    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public User() { }
    }
}