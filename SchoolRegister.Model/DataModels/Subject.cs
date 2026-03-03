using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SubjectGroup> SubjectGroups { get; set; }
    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public List<Grade> Grades { get; set; }

    public Subject(int id, string name, string description, int? teacherId = null)
    {
        Id = id;
        Name = name;
        Description = description;
        SubjectGroups = new List<SubjectGroup>();
        Teacher = null;
    }
}