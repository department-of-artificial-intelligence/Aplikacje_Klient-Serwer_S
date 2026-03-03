using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Group Group { get; set; }
    public Parent Parent { get; set; }
    public List<Grade> Grades { get; set; } = new List<Grade>();

    // Calculate AverageGrade
    public double AverageGrade
    {
        get
        {
            if (Grades.Count == 0)
                return 0;

            double sum = 0;
            foreach (var grade in Grades)
            {
                sum += grade.GradeValue;
            }

            return sum / Grades.Count;
        }
    }

    // Calculate AverageGradePerSubject
    public Dictionary<string, double> AverageGradePerSubject
    {
        get
        {
            var averagePerSubject = new Dictionary<string, double>();
            var subjectGrades = new Dictionary<string, List<int>>();

            foreach (var grade in Grades)
            {
                if (!subjectGrades.ContainsKey(grade.Subject.Name))
                    subjectGrades[grade.Subject.Name] = new List<int>();

                subjectGrades[grade.Subject.Name].Add(grade.GradeValue);
            }

            foreach (var subject in subjectGrades)
            {
                double sum = 0;
                foreach (var grade in subject.Value)
                {
                    sum += grade;
                }
                averagePerSubject[subject.Key] = sum / subject.Value.Count;
            }

            return averagePerSubject;
        }
    }

    // Calculate GradesPerSubject
    public Dictionary<string, List<int>> GradesPerSubject
    {
        get
        {
            var gradesPerSubject = new Dictionary<string, List<int>>();
            foreach (var grade in Grades)
            {
                if (!gradesPerSubject.ContainsKey(grade.Subject.Name))
                    gradesPerSubject[grade.Subject.Name] = new List<int>();

                gradesPerSubject[grade.Subject.Name].Add(grade.GradeValue);
            }
            return gradesPerSubject;
        }
    }
}