using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels
{
    public class Teacher : User
    {
        public string Title { get; set;}
        public virtual  IList<Subject> Subjects {get;set;}= new List<Subject>();
        public Teacher(): base()
        {
            Subjects= new List<Subject>();
        }
    }
}