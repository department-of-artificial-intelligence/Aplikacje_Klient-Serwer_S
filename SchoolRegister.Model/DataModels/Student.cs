using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

        [NotMapped]
        public double AverageGrade =>
            Grades != null && Grades.Any() ? Math.Round(Grades.Average(grade_item => (int)grade_item.GradeValue), 1) : 0.0;

        [NotMapped]
        public IDictionary<string, double> AverageGradePerSubject =>
            Grades != null ? Grades
                .GroupBy(grade_item => grade_item.Subject.Name)
                .Select(group_item => new {
                    subject_name = group_item.Key,
                    avg_grade = Math.Round(group_item.Average(avg_item => (int)avg_item.GradeValue), 1)
                })
                .ToDictionary(dict_item => dict_item.subject_name, dict_item => dict_item.avg_grade)
            : new Dictionary<string, double>();

        [NotMapped]
        public IDictionary<string, List<GradeScale>> GradesPerSubject =>
            Grades != null ? Grades
                .GroupBy(grade_item => grade_item.Subject.Name)
                .Select(group_item => new {
                    subject_name = group_item.Key,
                    grade_list = group_item.Select(x_item => x_item.GradeValue).ToList()
                })
                .ToDictionary(dict_item => dict_item.subject_name, dict_item => dict_item.grade_list)
            : new Dictionary<string, List<GradeScale>>();

        public Student() { }
    }
}