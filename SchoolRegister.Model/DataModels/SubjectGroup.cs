using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
    [Key]
    public int SubjectId { get; set; }


    public Subject Subject { get; set; } = null!;

    [Key]
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public SubjectGroup()
    {
    }
}