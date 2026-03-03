using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public IList<Subject> Subjects { get; set; } = null!;
    public string Title {get; set;} = null!;
    
    
}
