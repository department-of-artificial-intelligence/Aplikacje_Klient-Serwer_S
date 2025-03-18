using System.Security.Cryptography.X509Certificates;

public class Grade
{
    public DateTime DateOfIssue {get;set;} = DateTime.Now;
    public GradeScale GradeValue {get; set;}

    public Subject Subject {get; set;} = null!;

    public int SubjectId {get; set;}

    public int StudentId {get; set; }

    public Student Student {get;set;} = null!;

    
}