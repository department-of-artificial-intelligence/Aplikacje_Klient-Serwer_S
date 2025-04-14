using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
namespace SchoolRegister.Model.DataModels;
public enum GradeScale : int
{
    NDST =2,
    DST=3,
    DB = 4,
    BDB =5
}