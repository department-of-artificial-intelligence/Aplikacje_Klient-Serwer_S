using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels

{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Student> Students { get; set; }
        public virtual IList<SubjectGroup> SubjectGroups { get; set; } 

        public Group()
        {
            Students = new List<Student>();
            SubjectGroups = new List<SubjectGroup>();
        }
    }
}