using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels;


public class Student : User
{
    public virtual Group? Group { get; set; }
    [ForeignKey("Group")]
    public int? GroupId { get; set; }
    public virtual IList<Grade> Grades { get; set; } = default!;
    public virtual Parent? Parent { get; set; }
    [ForeignKey("Parent")]
    public int? ParentId { get; set; }
    [NotMapped]
    public double AverageGrade
    {
        get
        {
            if(Grades == null || !Grades.Any()) return 0.0d;
            return Grades.Average(g => (int)g.GradeValue);
        }
    }
    [NotMapped]
    public IDictionary<string, double> AverageGradePerSubject 
    {
        get
        {
            return Grades
            .GroupBy(g => g.Subject.Name)
            .ToDictionary(gr => gr.Key, gr => gr
            .Average(g => (int)g.GradeValue));
        } 
    }
    [NotMapped]
    public IDictionary<string, List<GradeScale>> GradePerSubject 
    { 
        get
        {
            return Grades
            .GroupBy(g => g.Subject.Name)
            .ToDictionary(gr => gr.Key, gr => gr
            .Select(g => g.GradeValue)
            .ToList());   
        }
    }
    public Student() : base() { }

}