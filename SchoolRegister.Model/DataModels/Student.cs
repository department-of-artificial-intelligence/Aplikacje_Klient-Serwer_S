namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public int? GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int? ParentId { get; set; }
    public Parent Parent { get; set; } = null!;

    public IList<Grade> Grades { get; set; }

    public double AverageGrade
    {
        get
        {
            if (Grades == null)
            {
                return 0;
            }

            double avg = Grades.Average(x => (int)x.GradeValue);
            return avg;
        }
    }
    public IDictionary<string, double> AverageGradePerSubject
    {
        get
        {
            IDictionary<string, double> avgps = Grades.GroupBy(x => x.Subject.Name).ToDictionary(x => x.Key, x => x.Average(x => (int)x.GradeValue));
            return avgps;
        }
    }
    public IDictionary<string, List<GradeScale>> GradesPerSubject
    {
        get
        {
            IDictionary<string, List<GradeScale>> gps = Grades.GroupBy(x => x.Subject.Name).ToDictionary(x => x.Key, x => x.Select(x => x.GradeValue).ToList());
            return gps;
        }
    }

    public Student()
    {
        Grades = new List<Grade>();
    }
}

