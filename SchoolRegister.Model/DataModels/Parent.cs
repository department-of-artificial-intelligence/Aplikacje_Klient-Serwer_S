using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataModels
{
    public class Parent : User
    {
        public virtual IList<Student> Students { get; set; }

        public Parent()
        {
            Students = new List<Student>();
        }
    }
}