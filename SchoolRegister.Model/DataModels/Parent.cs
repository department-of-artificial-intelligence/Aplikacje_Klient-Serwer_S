using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class Parent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Children { get; set; }

    public Parent()
    {
        Children = new List<Student>();
    }
}