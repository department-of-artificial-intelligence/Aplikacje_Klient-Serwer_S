using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; } = null!;
    public int? GroupId { get; set; } = null!;
    public IList<Grade> Grades { get; set; } = new List<Grade>();
    public Parent Parent { get; set; } = null!;
    public int? ParentId { get; set; } = null!;
    public double AverageGrade
    {
        get
        {
            Grades.Average(g => (double)g.GradeValue, 2);
        }
    }
    public IDictionary<string, double> AverageGradePerSubject { get; } = null!;
    public IDictionary<string, List<GradeScale>> GradePerSubject { get; } = null!;

    public Student()
    {
        Grades = new List<Grade>();
        AverageGradePerSubject = new Dictionary<string, double>();
        GradesPerSubject = new Dictionary<string, List<GradeScale>>();
    }

}
