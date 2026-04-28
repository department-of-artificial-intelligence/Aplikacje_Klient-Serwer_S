using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;

    public int? TeacherId { get; set; }
    public virtual Teacher? Teacher { get; set; }

    public virtual IList<SubjectGroup> SubjectGroups { get; set; }
    public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

    public Subject()
    {
        SubjectGroups = new List<SubjectGroup>();
        Grades = new List<Grade>();
    }
}