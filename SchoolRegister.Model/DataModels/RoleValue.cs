using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;
public enum RoleValue : int
{
    User = 0,
    Student = 1,
    Parent = 2,
    Teacher = 3,
    Admin = 4
}