using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Text.RegularExpressions;
namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; }= null!;
    public int GroupId{
        get; set;
    }

    public IList<Grade> Grades {get; set;}= null!;

    public Parent Parent {get; set;}= null!;
    
    public int ParentId {get; set;}
    public double AverageGrade => Grades?.Any() == true ? Grades.Average(g => (int)g.GradeValue) : 0;
    public IDictionary<string, double> AverageGradePerSubject => 
    Grades?
        .GroupBy(g => g.Subject.Name)
        .ToDictionary(g => g.Key, g => g.Average(grade => (int)grade.GradeValue)) ?? new Dictionary<string, double>();
    public IDictionary<string, List<GradeScale>> GradesPerSubject => 
    Grades?
        .GroupBy(g => g.Subject.Name)
        .ToDictionary(g => g.Key, g => g.Select(grade => grade.GradeValue).ToList()) ?? new Dictionary<string, List<GradeScale>>();


}