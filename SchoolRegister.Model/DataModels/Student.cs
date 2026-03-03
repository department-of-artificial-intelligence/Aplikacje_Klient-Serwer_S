using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels;

public class Student
{
    public Group Group { get; set; }
    public int GroupId { get; set; }
    public List<Grade> Grades { get; set; }
    public Parent Parent { get; set; }
    public int ParentId { get; set; }
    public double AverageGrade { get; set; }
    public Dictionary<string, double> AverageGradePerSubject { get; set; }
    public Dictionary<string, List<GradeScale>> GradesPerSubject { get; set; }

    public Student()
    {
        Grades = new List<Grade>();
        AverageGradePerSubject = new Dictionary<string, double>();
        GradesPerSubject = new Dictionary<string, List<GradeScale>>();
    }
}