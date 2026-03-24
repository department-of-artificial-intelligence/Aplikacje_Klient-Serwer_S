using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
    public int SubjectId { get; set; }


    public virtual Subject Subject { get; set; } = null!;

    public int GroupId { get; set; }
    public virtual Group Group { get; set; } = null!;

    public SubjectGroup()
    {
    }
}