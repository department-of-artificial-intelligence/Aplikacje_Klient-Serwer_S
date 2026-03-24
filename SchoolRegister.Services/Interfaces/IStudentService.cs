using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.ConcreteServices;

namespace SchoolRegister.Services.Interfaces;

    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null);
    }

