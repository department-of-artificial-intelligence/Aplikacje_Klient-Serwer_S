using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Services.Interfaces
{
    
    public interface IGradeService
    {
        GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVM);
        GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm);
        
    }
    
}