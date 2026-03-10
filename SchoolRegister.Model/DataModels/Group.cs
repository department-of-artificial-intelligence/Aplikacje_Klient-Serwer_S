using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public IList<Student> Students { get; set; }
    public IList<SubjectGroup> SubjectGroups { get; set; }

    public Group()
    {
        Students = new List<Student>();
        SubjectGroups = new List<SubjectGroup>();
    }
}