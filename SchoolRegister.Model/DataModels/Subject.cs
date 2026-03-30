using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
    
        [Key]
        public int Id{get;set;}
        [Required]

        public string Name{get;set;}
        public string Description{get;set;}
        public int? TeacherId {get;set;}
        public virtual Teacher Teacher{get;set;}
        public virtual IList<SubjectGroup> SubjectGroups { get; set; } 
        public virtual IList<Grade> Grades { get; set; }
        [NotMapped]
        public double AverageGrade => Math.Round(Grades.Average(g=>(int)g.GradeValue),1);
        public Subject()
        {
            SubjectGroups= new List<SubjectGroup>();
            Grades = new List<Grade>();
        }
    }
}

