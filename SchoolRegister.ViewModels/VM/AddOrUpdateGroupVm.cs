using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateGroupVm
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public int SubjectId { get; set; }
    }
}