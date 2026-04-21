
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM;
public class AddGradeToStudentVm
{
    public int StudentId {get; set;}
    public int SubjectId {get; set;}
    [Required]
    public double Value {get; set;}
    public int TeacherId {get; set;}
}