using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<Student> Students = new List<Student>();
    public IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public Group()
    {
        Students = new List<Student>();
        SubjectGroups = new List<SubjectGroup>();
    }


}

