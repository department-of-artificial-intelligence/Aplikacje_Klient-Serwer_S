using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class SubjectGroup
    {
        public Subject subject { get; set; }
        public int SubjectID { get; set; }
        public Group group { get; set; }
        public int GroupID { get; set; }

        public SubjectGroup()
        {

        };

    }
}