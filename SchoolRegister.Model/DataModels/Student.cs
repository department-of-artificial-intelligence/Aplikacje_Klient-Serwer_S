using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            if (Grades.Count == 0)
                return 0;

            return Grades.Average(g => (double)g.GradeValue);
        }
    }

    public IDictionary<string, double> AverageGradePerSubject
    {
        get
        {
            return Grades?
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(x => (double)x.GradeValue)
                ) ?? new Dictionary<string, double>();
        }
    }

    public IDictionary<string, List<GradeScale>> GradesPerSubject
    {
        get
        {
            return Grades?
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.GradeValue).ToList()
                ) ?? new Dictionary<string, List<GradeScale>>();
        }
    }

    public Student()
    {
    }
}