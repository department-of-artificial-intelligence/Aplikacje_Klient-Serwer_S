using SchoolRegister.Model.DataModels;
using System.Collections.Generic;
using System.Linq;

public class Student : User
{
    public virtual Group? Group { get; set; }
    public int? GroupId { get; set; }

    public virtual Parent? Parent { get; set; }
    public int? ParentId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public double AverageGrade => Grades == null || Grades.Count == 0
        ? 0
        : Grades.Average(g => g.GradeValue);

    public Dictionary<string, double> AverageGradePerSubject =>
        Grades == null
        ? new Dictionary<string, double>()
        : Grades
            .GroupBy(g => g.Subject.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Average(x => x.GradeValue)
            );
}