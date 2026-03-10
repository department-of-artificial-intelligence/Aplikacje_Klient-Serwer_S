using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int? ParentId { get; set; }
        public Parent Parent { get; set; } = null!;

        public IList<Grade> Grades { get; set; } = new List<Grade>();

        // Średnia ocen
        public double AverageGrade()
        {
            return Grades.Any() ? Grades.Average(g => (double)g.GradeValue) : 0;
        }

        // Średnia ocena per przedmiot
        public IDictionary<string, double> AverageGradePerSubject()
        {
            return Grades
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(g => g.Key, g => g.Average(x => (double)x.GradeValue));
        }

        // Wszystkie oceny per przedmiot
        public IDictionary<string, List<GradeScale>> GradesPerSubject()
        {
            return Grades
                .GroupBy(g => g.Subject.Name)
                .ToDictionary(g => g.Key, g => g.Select(x => x.GradeValue).ToList());
        }

        public Student()
        {
            Grades = new List<Grade>();
        }
    }
}