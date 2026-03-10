using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int? TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public virtual Teacher? Teacher { get; set; }

    public virtual IList<SubjectGroup> SubjectGroups { get; set; }
    public virtual IList<Grade> Grades { get; set; }

    public Subject()
    {
        SubjectGroups = new List<SubjectGroup>();
        Grades = new List<Grade>();
    }
}
