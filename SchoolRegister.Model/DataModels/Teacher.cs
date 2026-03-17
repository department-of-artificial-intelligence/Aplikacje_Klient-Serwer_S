using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Teacher : User
    {
        public string Title { get; set; }
        public virtual IList<Subject> Subjects { get; set; }

        public Teacher()
        {
            Subjects = new List<Subject>();
        }
    }
}