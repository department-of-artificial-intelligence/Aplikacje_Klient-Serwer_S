using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>>? filterExpression);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filterExpression = null);
    }
}