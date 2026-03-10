using Microsoft.AspNetCore.Identity;
using System;
namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; }
    public int? GroupId { get; set; }
    public IList<Grade> Grades { get; set; }
    public Parent Parent { get; set; }
    public int? ParentId { get; set; }

    public double AverageGrade { 
        get{}
    }
    public IDictionary<string, double> AverageGradePerSubject { 
        get{}
    }
    public IDictionary<string, List<GradeScale>> GradesPerSubject { 
        get{}
    }

    public Student()
    {
        Grades = new List<Grade>();
        AverageGradePerSubject = new Dictionary<string, double>();
        GradesPerSubject = new Dictionary<string, List<GradeScale>>();
    }





}