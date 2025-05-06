using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class TeacherVm
    {
        public string Title {get; set;} = null!;
        public IList<Grade> Grades {get; set;} = new List<Grade>();
    }
}