using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = "Jan";
    public virtual IList<Student> Students { get; set; }
    public IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();
}