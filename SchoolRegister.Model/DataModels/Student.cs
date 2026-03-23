using System.Collections.Generic;
using System.Linq;

namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public int? GroupId { get; set; }
    public virtual Group Group { get; set; } = null!;
    
    public int? ParentId { get; set; }
    public virtual Parent Parent { get; set; } = null!;
    
    public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

    
    public double AverageGrade => Grades.Any() ? Grades.Average(g => (int)g.GradeValue) : 0.0;

    public IDictionary<string, double> AverageGradePerSubject => 
        Grades.GroupBy(g => g.Subject.Name)
              .ToDictionary(gr => gr.Key, gr => gr.Average(g => (int)g.GradeValue));

    public IDictionary<string, List<GradeScale>> GradesPerSubject => 
        Grades.GroupBy(g => g.Subject.Name)
              .ToDictionary(gr => gr.Key, gr => gr.Select(g => g.GradeValue).ToList());
}