using SchoolRegister.Model.DataModels;
using System.Collections.Generic;

public class Parent : User
{
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}