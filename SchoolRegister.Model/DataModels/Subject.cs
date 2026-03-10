using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.Model.DataModels;

public class Subject
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public IList<SubjectGroup> SubjectGroups { get; set; }

    public Teacher Teacher { get; set; }

    public int? TeacherId { get; set; }

    public IList<Grade> Grades { get; set; }

    public Subject() { }
}