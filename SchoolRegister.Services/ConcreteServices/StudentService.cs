using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging; // Zalecane podejście w .NET Core / ASP.NET Core
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class StudentService : BaseService, IStudentService
{
    public StudentService(
        ApplicationDbContext dbContext, 
        IMapper mapper, 
        ILogger logger) 
        : base(dbContext, mapper, logger)
    {
    }

    public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
    {
        // TODO: Dodaj logikę pobierania pojedynczego ucznia
        throw new NotImplementedException();
    }

    public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
    {
        // TODO: Dodaj logikę pobierania listy uczniów
        throw new NotImplementedException();
    }
}