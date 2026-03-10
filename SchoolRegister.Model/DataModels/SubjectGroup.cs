using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
    [Required]
    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public virtual Subject? Subject { get; set; }

    [Required]
    public int GroupId { get; set; }
    [ForeignKey("GroupId")]
    public virtual Group? Group { get; set; }
}
