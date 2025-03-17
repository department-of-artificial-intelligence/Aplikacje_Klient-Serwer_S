using System.Text.RegularExpressions;

namespace SchoolRegister.Model.DataModels{
    public class Student : User{
        public Group Group {get; set;} = null!;
        public int? GroupId {get; set;}
        public IList<Grade> Grades {get; set;} = new List<Grade>();
        public Parent Parent {get; set;} = null!;
        public int? ParentId {get; set;}

        public double AverageGrade => Grades.Any() ? Grades.Average(g => (int)g.GradeValue) : 0;
        public IDictionary<String, double> AverageGradePerSubject =>
            Grades.GroupBy(g => g.Subject.Name)
            .ToDictionary(g => g.Key, g => g.Average(grade => (int)grade.GradeValue));

        public IDictionary<string, List<GradeScale>> GradesPerSubject =>
            Grades.GroupBy(g => g.Subject.Name)
            .ToDictionary(g => g.Key, g => g.Select(grade => grade.GradeValue).ToList());

        public Student() {}
    }
}