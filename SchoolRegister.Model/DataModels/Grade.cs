using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
    public DateTime DateOfIssue { get; set; }
    public GradeScale GradeValue { get; set; }
    public virtual Subject Subject { get; set; }
    public int SubjectId { get; set; }
    public virtual Student Student { get; set; }
    public int? StudentId { get; set; }
}