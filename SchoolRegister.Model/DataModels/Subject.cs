using System.ComponentModel;

namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        IList<SubjectGroup> SubjectGroups { get; set; } = null!;
        Teacher Teacher { get; set; } = null!;
        public int? TeacherId { get; set; }
        IList<Grade> Grades { get; set; } = null!;

    }
}