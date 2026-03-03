using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;

public class Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Subject> Subjects { get; set; } = new List<Subject>();
}