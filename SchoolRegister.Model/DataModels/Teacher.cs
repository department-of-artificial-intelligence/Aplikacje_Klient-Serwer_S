using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public string Title {get; set;} = null!;
    public IList<Subject> Subjects {get; set;} = null!;

    public Teacher() : base()
    {
        Subjects = new List<Subject>();
        Title = string.Empty;
    }
}