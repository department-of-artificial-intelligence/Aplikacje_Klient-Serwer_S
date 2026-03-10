using System.Collections.Generic;
using System.Linq;
using System;
public class Parent : User
{
    public IList<Student> Students { get; set; }

    public Parent()
    {
        Students = new List<Student>();
    }
}