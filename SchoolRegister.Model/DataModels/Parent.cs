using System.Collections.Generic;

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