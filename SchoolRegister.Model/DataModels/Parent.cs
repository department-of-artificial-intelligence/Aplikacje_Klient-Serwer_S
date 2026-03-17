using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Parent : User
{
    public virtual IList<Student> Students { get; set; } = new List<Student>();
    public Parent()
    {
        Students = new List<Student>();
    }


}
