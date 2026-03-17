using System.ComponentModel.DataAnnotations.Schema;
namespace DataModels
{
    public class SubjectGroup
    {
        public virtual Subject Subject { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Group Group { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public SubjectGroup() { }
    }
}