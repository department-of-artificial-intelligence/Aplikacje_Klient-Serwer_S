using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    public class Grade
    {
        [Key] // Definiuje klucz g³ówny dla tabeli ocen 
        public int Id { get; set; }

        public DateTime DateOfIssue { get; set; }

        public GradeScale GradeValue { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public Grade()
        {
            var current_date = DateTime.UtcNow;
            DateOfIssue = current_date;
        }
    }
}