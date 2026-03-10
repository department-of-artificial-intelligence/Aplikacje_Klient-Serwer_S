using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; } = null!;
    public int? GroupId { get; set; }
    public IList<Grade> Grades { get; set; } = new List<Grade>();
    public Parent Parent { get; set; } = null!;

    public int? ParentId { get; set; }

    public double AverageGrade
    {
        get
        {
            if (Grades == null || !Grades.Any()) return 0.0;
            return Grades.Average(g => (double)g.GradeValue);
        }
    }
    public IDictionary<string, double> AverageGradePerSubject
    {
        get
        {
            return Grades.GroupBy(g => g.Subject.Name).ToDictionary(group => group.Key, group => group.Average(g => (double)g.GradeValue));
        }
    }

    public IDictionary<string, List<GradeScale>> GradesPerSubject
    {
        get
        {
            return Grades.GroupBy(g => g.Subject.Name).ToDictionary(group => group.Key, Group => Group.Select(g => g.GradeValue).ToList());
        }
    }
    public Student()
    {
        Grades = new List<Grade>();
    }

}
