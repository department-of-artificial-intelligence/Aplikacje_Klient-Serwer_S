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
    public double AverageGrade {get;}
    public IDictionary<string, double> AverageGradePerSubject {get;}= null!;
    public IDictionary<string, List<GradeScale>> GradesPerSubject {get;} = null!;

}