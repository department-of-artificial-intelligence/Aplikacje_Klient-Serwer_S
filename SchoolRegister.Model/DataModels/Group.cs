namespace SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Group
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual IList<Student> Students { get; set; }
    public virtual IList<SubjectGroup> SubjectGroups { get; set; }

    public Group()
    {
        Students = new List<Student>();
        SubjectGroups = new List<SubjectGroup>();
    }
}