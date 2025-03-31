using System.ComponentModel.DataAnnotations;

public class Subject
{
    [Key]
    public int Id {get; set; }

    [Required]
    public string Name {get; set;} = null!;

    public string Description {get;set;} = null!;

    public IList<SubjectGroup> SubjectGroups {get; set;} = null!;

    public Teacher Teacher {get;set;} = null!;

    public int? TeacherId {get;set;}

    public IList<Grade> Grades{get;set;} = null!;
}