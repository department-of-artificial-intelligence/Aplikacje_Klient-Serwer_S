using Microsoft.AspNetCore.Identity;
using System;
using System.Runtime.CompilerServices;
namespace SchoolRegister.Model.DataModels;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IList<SubjectGroup> SubjectGroups { get; set; }
    public Teacher Teacher { get; set; }
    public int? TeacherId { get; set; }
    public IList<Grade> Grades { get; set; }
    public Subject(int id, string name, string description, Teacher teacher, int? teacherId)
    {
        Id = id;
        Name = name;
        Description = description;
        Teacher = teacher;
        TeacherId = teacherId ?? 0;
        SubjectGroups = new List<SubjectGroups>;
        Grades = new List<Grade>;
    }
}