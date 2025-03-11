using System;


namespace SchoolRegister.Model.DataModels
{
    public class Parent : User
    {
        public required IList<Student> Students {get; set;}
    }
}