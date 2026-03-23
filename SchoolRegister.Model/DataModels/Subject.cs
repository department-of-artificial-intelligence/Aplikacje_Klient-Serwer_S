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

    public virtual IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();

    public virtual Teacher? Teacher { get; set; }

    public int? TeacherId { get; set; }

    public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
}