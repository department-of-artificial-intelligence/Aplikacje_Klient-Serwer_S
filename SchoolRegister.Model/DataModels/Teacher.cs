using System.Collections.Generic;

public class Teacher : User
{
    public string Title { get; set; }
    public IList<Subject> Subjects { get; set; }

    public Teacher()
    {
        Subjects = new List<Subject>();
    }
}