using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM;
public class TeacherVm
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public IList<SubjectVm> Subjects { get; set; } = new List<SubjectVm>();
}