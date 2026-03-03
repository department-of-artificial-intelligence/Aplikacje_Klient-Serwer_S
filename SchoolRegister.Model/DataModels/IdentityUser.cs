using System;

public class IdentityUsers<T>
{
    public T Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegistrationDate { get; set; }
}