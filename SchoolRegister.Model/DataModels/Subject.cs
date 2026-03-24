using System.Collections.Generic;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}