using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
{
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int ParentId { get; set; }
    public Parent Parent { get; set; }

    public IList<Grade> Grades { get; set; } = new List<Grade>();

    public double AverageGrade =>
        Grades.Any() ? Grades.Average(g => (int)g.GradeValue) : 0;

    public Dictionary<string, double> AverageGradePerSubject =>
        Grades
            .GroupBy(g => g.Subject.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Average(x => (int)x.GradeValue)
            );

    public Dictionary<string, IList<GradeScale>> GradesPerSubject =>
        Grades
            .GroupBy(g => g.Subject.Name)
            .ToDictionary(
                g => g.Key,
                g => (IList<GradeScale>)g.Select(x => x.GradeValue).ToList()
            );

    public Student() { }
}
}