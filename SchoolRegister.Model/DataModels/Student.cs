using System.Collections.Generic;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public Group Group { get; set; }
    public int? GroupId { get; set; }
    public IList<Grade> Grades { get; set; }
    public Parent Parent { get; set; }
    public int? ParentId { get; set; }
    public double AverageGrade { get; set; }
    public IDictionary<string, double> AverageGradePerSubject { get; set; }
    public IDictionary<string, List<GradeScale>> GradesPerSubject { get; set; }

    public Student() : base() { }
}