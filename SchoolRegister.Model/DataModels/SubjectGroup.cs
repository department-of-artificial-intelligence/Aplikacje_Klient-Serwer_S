using System;
using Subject;
using Group;
using System.Text.RegularExpressions;

namespace SchoolRegister.Model.DataModels;
public class SubjectGroup
{
    public SubjectGroup Subject {get; set;}
    public int SubjectId {get; set;}
    public Group Group {get; set;}
    public int GroupId {get; set;}
    public SubjectGroup()
    {
        
    }

}