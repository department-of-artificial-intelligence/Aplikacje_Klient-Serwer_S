using System;
using System.Collections.Generic;

public class Parent
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
}