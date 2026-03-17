using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public virtual int Id { get; set; }
    [Required]
    public virtual string Name { get; set; } = null!;

    public virtual string Description { get; set; } = null!;

    public virtual int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public IList<SubjectGroup> SubjectGroups { get; set; }
    public IList<Grade> Grades { get; set; }

    public Subject()
    {
        SubjectGroups = new List<SubjectGroup>();
        Grades = new List<Grade>();
    }
}