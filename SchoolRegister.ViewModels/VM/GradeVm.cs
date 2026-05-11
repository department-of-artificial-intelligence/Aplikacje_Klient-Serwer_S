namespace SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;
 public class GradeVm
 {
        public int Id { get; set; }
        public GradeScale GradeValue { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int StudentId { get; set; }
    }