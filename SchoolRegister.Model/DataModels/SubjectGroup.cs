using System.Text.RegularExpressions;
using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels;


public class SubjectGroup
{
    public Subject Subject { get; set; }
    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    public virtual Group Group { get; set; }
    [ForeignKey("Group")]
    public int? GroupId { get; set; }

    public SubjectGroup() { }
}