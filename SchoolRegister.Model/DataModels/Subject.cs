using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Subject
{
    public int Id { get; set; }
    public string Description { get; set; }

    public virtual ICollection<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();


    [Key]
    public int SubjectId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [ForeignKey("Teacher")]
    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; }
}