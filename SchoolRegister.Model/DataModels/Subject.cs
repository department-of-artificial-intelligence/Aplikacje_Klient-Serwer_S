using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    public List<Grade> Grades { get; set; } = new List<Grade>();
}