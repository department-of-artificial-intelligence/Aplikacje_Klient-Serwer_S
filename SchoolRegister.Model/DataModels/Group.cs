using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Group
{
    [Key]
    public int Id {get;set;}
    public string Name {get;set;} = null!;

    public virtual IList<Student> Students {get;set;} = null!;

    [ForeignKey("SubjectGroup")]
    public virtual IList<SubjectGroup> SubjectGroups {get;set;} = null!;
}