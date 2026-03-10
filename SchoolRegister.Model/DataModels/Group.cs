using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    public List<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public Group()
    {
        Students = new List<Student>();
        SubjectGroups = new List<SubjectGroup>();
    }
}