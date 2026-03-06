using System.Collections.Generic;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
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
}