using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    public List<SubjectGroup> SubjectGroups {get; set;}

    public Group()
    {
        Students = new List<Student>();
    }
}