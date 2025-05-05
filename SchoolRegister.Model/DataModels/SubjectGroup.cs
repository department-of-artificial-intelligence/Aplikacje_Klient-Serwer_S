using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class SubjectGroup{
   public virtual Subject Subject {get;set;}
   [ForeignKey("Subject")]

   public int SubjectId {get;set;}
   public Group Group {get;set;} =null!;

   public int GroupId {get;set;}

}