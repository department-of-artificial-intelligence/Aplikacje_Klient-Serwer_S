using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public string Title { get; set; }
    public IList<Subject> Subjects { get; set; }
    public Teacher() : base()
    {
        Subjects = new List<Subject>();
        Title = string.Empty;
    }
}