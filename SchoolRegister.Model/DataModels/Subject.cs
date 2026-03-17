using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();

    public Teacher Teacher { get; set; } = null!;

    public int? TeacherId { get; set; }

    public IList<Grade> Grades { get; set; } = new List<Grade>();
}