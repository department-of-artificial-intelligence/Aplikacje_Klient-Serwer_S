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
    public double AverageGrade { get; }
    public IDictionary<string, double> AverageGradePerSubject { get; } = new Dictionary<string, double>();
    public IDictionary<string, List<GradeScale>> GradesPerSubject { get; } = new Dictionary<string, List<GradeScale>>();



}