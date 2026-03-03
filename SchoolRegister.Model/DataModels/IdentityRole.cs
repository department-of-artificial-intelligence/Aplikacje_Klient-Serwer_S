using System;

public class IdentityUser<T>
{
    public T Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegistrationDate { get; set; }
}