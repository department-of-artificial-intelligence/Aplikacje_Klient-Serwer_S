namespace SchoolRegister.Model.DataModels;

public class Student : User
{
    public int? GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int? ParentId { get; set; }
    public Parent Parent { get; set; } = null!;

    public IList<Grade> Grades { get; set; }

    public double AverageGrade { 
        get{
            //zrobić zwracanie 
        }
    }
    public IDictionary<string, double> AverageGradePerSubject { 
        get{
            //zrobić zwracanie
        }
    }
    public IDictionary<string, List<GradeScale>> GradesPerSubject { 
        get{
            //zrobić zwracanie 
        }
    }

    public Student()
    {
        Grades = new List<Grade>();
        AverageGradePerSubject = new Dictionary<string, double>();
        GradesPerSubject = new Dictionary<string, List<GradeScale>>();
    }
}