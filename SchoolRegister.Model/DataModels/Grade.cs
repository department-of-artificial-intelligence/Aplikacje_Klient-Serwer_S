using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataModels
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public GradeScale GradeValue { get; set; }
        public virtual Subject Subject { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Grade() { }
    }
}