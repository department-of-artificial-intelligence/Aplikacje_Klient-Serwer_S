using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM;

public class GroupVm
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string GroupName => Name;
    public int? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public IList<StudentVm> Students { get; set; } = null!;
    public IList<SubjectVm> Subjects { get; set; } = null!;
}