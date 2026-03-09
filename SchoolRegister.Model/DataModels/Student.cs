using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; } = null!;
    public int? GroupId { get; set; } = null!;
    public IList<Grade> Grades { get; set; } = null!;
    public Parent Parent { get; set; } = null!;
    public int? ParentId { get; set; } = null!;
    public double AverageGrade { get; }
    public IDictionary<string, double> AverageGradePerSubject { get; } = null!;
    public IDictionary<string, List<GradeScale>> GradePerSubject { get; } = null!;

}
