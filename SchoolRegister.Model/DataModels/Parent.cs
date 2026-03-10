using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
public class Parent : User
{
    public IList<Student> Students { get; set; }

    public Parent()
    {
        Students = new List<Student>();
    }
}