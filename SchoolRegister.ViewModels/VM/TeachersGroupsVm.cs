using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class TeachersGroupsVm
    {
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; } = null!;

        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public string? TeacherName { get; set; }
        public string? GroupName { get; set; }
    }
}