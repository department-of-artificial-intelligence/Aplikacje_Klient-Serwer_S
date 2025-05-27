using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Subject
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    //[NotMapped]
    public string Description { get; set; } = null!;

    public virtual IList<SubjectGroup> SubjectGroups { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    [ForeignKey("Teacher")]
    public int? TeacherId { get; set; }

    [NotMapped]
    public virtual IList<Grade> Grades { get; set; } = null!;
}
