using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    public class Grade{
        public virtual DateTime DateOfIssue { get; set; } = DateTime.Now;
        public virtual GradeScale GradeValue { get; set; }
        public virtual Subject Subject { get; set; } = null!;
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = null!;

        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;
    }
}
