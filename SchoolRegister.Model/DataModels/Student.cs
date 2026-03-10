using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public int? GroupId { get; set; }
    [ForeignKey("GroupId")]
    public virtual Group? Group { get; set; }

    public int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public virtual Parent? Parent { get; set; }

    public virtual IList<Grade> Grades { get; set; }

    public double AverageGrade { get; }
    public IDictionary<string, double> AverageGradePerSubject { get; } = new Dictionary<string, double>();
    public IDictionary<string, List<GradeScale>> GradesPerSubject { get; } = new Dictionary<string, List<GradeScale>>();

    public Student()
    {
        Grades = new List<Grade>();
    }
}
