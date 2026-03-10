using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual IList<Student> Students { get; set; } = null!;
    public IList<SubjectGroup> SubjectGroups { get; set; } = null!;

}
