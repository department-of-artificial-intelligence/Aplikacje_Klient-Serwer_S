using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions; // Dodane dla .Average() i .Any()

namespace SchoolRegister.Model.DataModels
{
    public class Subject
{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")] // Opcjonalne, ale warto doprecyzować
        public virtual Teacher Teacher { get; set; }

        public virtual IList<SubjectGroup> SubjectGroups { get; set; } 
        public virtual IList<Grade> Grades { get; set; }

        [NotMapped]
        public double AverageGrade => (Grades != null && Grades.Any()) 
            ? Math.Round(Grades.Average(g => (int)g.GradeValue), 1) 
            : 0;

        public Subject()
        {
            SubjectGroups = new List<SubjectGroup>();
            Grades = new List<Grade>();
        }
    }
}