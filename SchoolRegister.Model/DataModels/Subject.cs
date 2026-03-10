using System.Collections.Generic;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
        public Subject(int id, string description) 
        {
            this.Id = id;
    this.Description = description;
   
        }
            public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public IList<SubjectGroup> SubjectGroups { get; set; }
    public IList<Grade> Grades { get; set; }

    public Subject()
    {
        SubjectGroups = new List<SubjectGroup>();
        Grades = new List<Grade>();
    }
}namespace SchoolRegister.Model.DataModels
{
    public enum RoleValue
    {
        User = 0,
        Student = 1,
        Parent = 2,
        Teacher = 3,
        Admin = 4
    }
}