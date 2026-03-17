using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public virtual IList<Subject> Subjects { get; set; } = new List<Subject>();
    public string Title { get; set; } = null!;
    public Teacher()
    {
        Subjects = new List<Subject>();
    }


}

