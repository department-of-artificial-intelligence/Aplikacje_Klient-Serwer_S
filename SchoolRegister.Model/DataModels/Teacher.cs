using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public IList<Subject> Subjects { get; set; } = new List<Subject>();

    public string Title { get; set; } = string.Empty;

    public Teacher()
    {
    }
}