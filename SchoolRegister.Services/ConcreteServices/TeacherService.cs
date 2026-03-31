using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
// Zwróć uwagę na logger - w ASP.NET Core zazwyczaj używa się poniższego zamiast Castle.Core.Logging
using Microsoft.Extensions.Logging; 
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class TeacherService : BaseService, ITeacherService
{
    private readonly UserManager<User> _userManager;

    public TeacherService(
        ApplicationDbContext dbContext, 
        IMapper mapper, 
        ILogger logger, 
        UserManager<User> userManager) 
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
    {
        // TODO: Dodaj logikę pobierania pojedynczego nauczyciela
        throw new NotImplementedException();
    }

    public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
    {
        // TODO: Dodaj logikę pobierania listy nauczycieli
        throw new NotImplementedException();
    }

    public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
    {
        // TODO: Dodaj logikę pobierania grup nauczycieli
        throw new NotImplementedException();
    }
}