using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public class Student : User
{
    public Group Group { get; set; }
    public int GroupId { get; set; }
    public List<Grade> Grades { get; set; }
    public Parent Parent { get; set; }
    public int ParentId { get; set; }
    public double AverageGrade { get; private set; }
    public Dictionary<string, double> AverageGradePerSubject { get; private set; }
    public Dictionary<string, List<GradeScale>> GradesPerSubject { get; private set; }

    public Student()
    {
        Grades = new List<Grade>();
        AverageGradePerSubject = new Dictionary<string, double>();
        GradesPerSubject = new Dictionary<string, List<GradeScale>>();
    }

    public double GetAverageGrade()
    {
        if (Grades.Count == 0)
            return 0;

        AverageGrade = Grades.Average(g => g.GradeValue);
        return AverageGrade;
    }

    public Dictionary<string, double> GetAverageGradePerSubject()
    {
        return Grades
            .GroupBy(g => g.Subject)
            .ToDictionary(
                g => g.Key.Name,
                g => g.Average(x => x.GradeValue)
            );
    }
}