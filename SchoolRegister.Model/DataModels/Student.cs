using System.Collections.Generic;
using System.Linq;
namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public int? GroupId {get;set;}
        public virtual Group Group {get;set;}
        public int? ParentId {get; set;}
        public Parent Parent { get; set; }
        public virtual IList<Grade> Grades{get;set;}=new List<Grade>();
        public double AverageGrade
        {
            get
            {
                if (Grades==null || Grades.Count == 0) return 0;
                return Grades.Average(g => (int)g.GradeValue);
            }
        }
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

    public Student()
    {
        Grades = new List<Grade>();
    }

    }
}