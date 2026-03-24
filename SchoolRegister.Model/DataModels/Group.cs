using System.Collections.Generic;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public virtual ICollection<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
}