using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;
namespace SchoolRegister.ViewModels.VM;

public class GetGradesReportVm
{
    public int StudentId { get; set; }
    public int GetterUserId { get; set; }
}