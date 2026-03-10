using System.Collections.Generic;
using System.Linq;
using System;
public class Teacher : User
{
    public string Title { get; set; }
    public IList<Subject> Subjects { get; set; }

    public Teacher()
    {
        Subjects = new List<Subject>();
    }
}