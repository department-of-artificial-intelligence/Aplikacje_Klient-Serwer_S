using System.ComponentModel.DataAnnotations.Schema;
namespace DataModels
{
    public class Student : User
    {
        public virtual Group Group { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual Parent Parent { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
        [NotMapped]
        public double AverageGrade
        {
            get
            {
                if (Grades == null || Grades.Count == 0) return 0;
                return Grades.Average(g => (int)g.GradeValue);
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
                        group => group.Average(g => (int)g.GradeValue)
                    );
            }
        }
        public Student() { }
    }
}