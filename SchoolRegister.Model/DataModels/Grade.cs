using System;

namespace SchoolRegister.Model.DataModels
{
    public class Grade
    {
        public DateTime DateOfIssue {get; set;}
        public required GradeScale GradeValue {get; set;}
        public required Subject Subject {get; set;}
        public int SubjectId {get; set;}
        public int StudentId {get; set;}
        public required Student Student {get; set;}

    }
}