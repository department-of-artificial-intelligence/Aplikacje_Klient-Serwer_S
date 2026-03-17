using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual required IList<SubjectGroup> SubjectGroups { get; set; }
    public virtual required Teacher Teacher { get; set; }
    public int? TeacherId { get; set; }
    public virtual required IList<Grade> Grades { get; set; }

   
}