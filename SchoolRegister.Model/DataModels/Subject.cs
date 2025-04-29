using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    [Table("Subject")]
    public class Subject{
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;} = null!;
        public string Description {get;set;} = null!;
        public virtual IList<SubjectGroup> SubjectGroups {get; set;} = new List<SubjectGroup>();
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher {get; set;} = null!;
        [ForeignKey("Teacher")]
        public int? TeacherId {get; set;}
        public virtual IList<Grade> Grades {get; set;} = new List<Grade>();
    }
}