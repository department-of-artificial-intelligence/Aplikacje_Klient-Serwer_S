using System.Text.RegularExpressions;

public class Student
{
    public Group Group {get;set;} = null!;
    public int? GroupId {get;set;}

    public IList<Grade> Grades {get;set;} = null!;

    public Parent Parent {get;set;} = null!;

    public int? ParentId {get; set;}

    public double AverageGrade {get;}

    public IDictionary<string,double> AverageGradePerSubject {get; set;} = null!;

    public IDictionary<string, List<GradeScale>> GradesPerSubject { get; } = null!;
}