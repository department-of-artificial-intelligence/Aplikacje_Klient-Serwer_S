using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace SchoolRegister.Services.Interfaces
{
    public interface IGradeService
    {
        GradeVm AddGradeToStudent();
        GradesReportVm GetGradesReportForStudent();

    }
}