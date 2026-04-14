using SchoolRegister.ViewModels.VM;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null);
    }
}