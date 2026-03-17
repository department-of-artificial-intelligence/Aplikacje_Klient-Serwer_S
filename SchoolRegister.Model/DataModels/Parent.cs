using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    public class Parent : User
    {
        public virtual IList<Student> Student{get;set;}
        public Parent():base()
        {
            Student=new List<Student>();
        }
    }
}