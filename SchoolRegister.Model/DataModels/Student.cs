using System.Collections.Generic;
namespace SchoolRegister.Model.DataModels;
public class Student : User {
    public Group Group {get; set;}
    public int? GroupId {get; set;}
    public IList<Grade> Grades{get;set;}
    public Parent Parent{get;set;}
    public int? ParentId{get;set;}
    public double   AverageGrade {get;}
    public IDictionary<String, double> AverageGradePerSubject  {get;}
    public IDictionary<String, List<GradeScale>> GradesPerSubject  {get;}
    public Student(){}
}