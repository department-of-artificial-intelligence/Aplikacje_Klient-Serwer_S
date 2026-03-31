using System;

namespace SchoolRegister.ViewModels.VM;

public class GetGradesReportVm
{
    public int StudentId { get; set; }
    
    public int? SubjectId { get; set; } 
    
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}