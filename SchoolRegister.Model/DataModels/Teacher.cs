using System;
using User;
using Subject;

namespace SchoolRegister.Model.DataModels;
public class Teacher : User
{
    public IList<Subject> Subjects {get; set;}
    public string Title {get; set;}
    public Teacher()
    {
        
    }

}