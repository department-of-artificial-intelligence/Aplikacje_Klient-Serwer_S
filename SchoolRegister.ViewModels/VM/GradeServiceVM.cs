using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class AddGradeToStudentVm
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public GradeScale GradeValue { get; set; }
        public int TeacherId { get; set; }
    }

    public class GradeVm { }

    public class GetGradesReportVm
    {
        public int StudentId { get; set; }
        public int GetterUserId { get; set; }
    }

    public class GradesReportVm { }
}