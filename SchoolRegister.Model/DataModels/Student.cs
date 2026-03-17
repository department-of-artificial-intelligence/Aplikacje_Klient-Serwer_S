using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
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
            if (Grades.Count == 0) return 0;
            return Grades.Average(g => (double)g.GradeValue);
        }
    }
    public IDictionary<string, double> AverageGradePerSubject
    {
        get
        {
            var result = new Dictionary<string, double>();
            var subjects = Grades.Select(g => g.Subject.Name).Distinct();
            foreach (var subject in subjects)
            {
                var gradesForSubject = Grades.Where(g => g.Subject.Name == subject);
                result[subject] = gradesForSubject.Average(g => (double)g.GradeValue);
            }
            return result;
        }
    }
    public IDictionary<string, List<GradeScale>> GradesPerSubject
    {
        get
        {
            var result = new Dictionary<string, List<GradeScale>>();
            var subjects = Grades.Select(g => g.Subject.Name).Distinct();
            foreach (var subject in subjects)
            {
                var gradesForSubject = Grades.Where(g => g.Subject.Name == subject);
                result[subject] = gradesForSubject.Select(g => g.GradeValue).ToList();
            }
            return result;
        }
    }



}