using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>>? filterPredicate = null);
    }
}