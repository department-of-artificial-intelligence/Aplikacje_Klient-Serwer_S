namespace SchoolRegister.ViewModels.VM.cs
{
    

    public class AttachDetachStudentToGroupVm
    {
        public int GroupId { get; set; }
        public int StudentId { get; set; }
    }

    public class AttachDetachSubjectGroupVm
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }

    public class AttachDetachSubjectToTeacherVm
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}