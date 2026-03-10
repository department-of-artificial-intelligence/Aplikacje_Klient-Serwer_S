namespace SchoolRegister.Model.DataModels;
public class Student: User
{
    public Group Group {get; set;} = null!;
    public int? GroupId {get; set;} = null!;
    public IList<Grade> Grades { get; set; } = null!;
    public Parent Parent {get; set;} = null!;
    public int? ParentId {get; set;} = null!;

    public double AverageGrade => Grades.Any() ? Grades.Average(g => (double)g.GradeValue) : 0.0;

    public IDictionary<string, double> AverageGradePerSubject => Grades
        .GroupBy(g => g.Subject.Name)
        .ToDictionary(
            g => g.Key,
            g => g.Average(grade => (double)grade.GradeValue)
        );

    public IDictionary<string, List<GradeScale>> GradesPerSubject => Grades
        .GroupBy(g => g.Subject.Name)
        .ToDictionary(
            g => g.Key,
            g => g.Select(g => g.GradeValue).ToList()
        );
}