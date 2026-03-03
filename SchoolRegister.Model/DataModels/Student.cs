using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class Student : User
{
    public List<Grade> Grades { get; set; }
    public List<Group> Groups { get; set; }

    public Student()
    {
        Grades = new List<Grade>();
        Groups = new List<Group>();
    }
}