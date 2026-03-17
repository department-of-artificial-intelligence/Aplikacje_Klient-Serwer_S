using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolRegister.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public virtual Group? Group { get; set; }

        [ForeignKey("Group")]
        public int? GroupId { get; set; }

        public virtual Parent? Parent { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

        [NotMapped]
        public double AverageGrade
        {
            get
            {
                if (Grades == null || Grades.Count == 0) return 0;
                return System.Math.Round(Grades.Average(g => (int)g.GradeValue), 1);
            }
        }

        [NotMapped]
        public IDictionary<string, double> AverageGradePerSubject
        {
            get
            {
                if (Grades == null) return new Dictionary<string, double>();

                return Grades
                    .Where(g => g.Subject != null)
                    .GroupBy(g => g.Subject.Name)
                    .ToDictionary(
                        group => group.Key,
                        group => System.Math.Round(group.Average(g => (int)g.GradeValue), 1)
                    );
            }
        }

        [NotMapped]
        public IDictionary<string, List<GradeScale>> GradesPerSubject
        {
            get
            {
                if (Grades == null) return new Dictionary<string, List<GradeScale>>();

                return Grades
                    .Where(g => g.Subject != null)
                    .GroupBy(g => g.Subject.Name)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(g => g.GradeValue).ToList()
                    );
            }
        }
    }
}