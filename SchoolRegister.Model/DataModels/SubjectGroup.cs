using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
public class SubjectGroup
{
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public SubjectGroup() { }
}