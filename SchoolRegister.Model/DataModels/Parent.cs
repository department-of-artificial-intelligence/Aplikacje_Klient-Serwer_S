using System;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;

public class Parent:User
{
    public virtual IList<Student> Students{get;set;}
    
}