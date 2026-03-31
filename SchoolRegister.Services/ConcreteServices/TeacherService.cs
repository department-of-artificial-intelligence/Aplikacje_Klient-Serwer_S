using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class TeacherService : BaseService, ITeacherService
{
    private readonly UserManager<User> _userManager;

    public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression)
    {
        try
        {
            if (filterExpression == null)
                throw new ArgumentNullException("FilterExpression is null");
            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterExpression);
            return Mapper.Map<TeacherVm>(teacherEntity);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterExpression = null)
    {
        try
        {
            var teachers = DbContext.Users.OfType<Teacher>().AsQueryable();
            if (filterExpression != null)
                teachers = teachers.Where(filterExpression);
            return Mapper.Map<IEnumerable<TeacherVm>>(teachers);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public TeacherVm AddOrUpdateTeacher(AddOrUpdateTeacherVm addOrUpdateTeacherVm)
    {
        try
        {
            if (addOrUpdateTeacherVm == null)
                throw new ArgumentNullException("View model parameter is null");
            var teacherEntity = Mapper.Map<Teacher>(addOrUpdateTeacherVm);
            if (!addOrUpdateTeacherVm.Id.HasValue || addOrUpdateTeacherVm.Id == 0)
                DbContext.Users.Add(teacherEntity);
            else
                DbContext.Users.Update(teacherEntity);
            DbContext.SaveChanges();
            return Mapper.Map<TeacherVm>(teacherEntity);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }

    public TeacherGroupsVm GetTeacherGroups(Expression<Func<TeacherGroupsVm, bool>> filterExpression)
    {
        try
        {
            if (filterExpression == null)
                throw new ArgumentNullException("FilterExpression is null");
            var teacherGroupsVms = Mapper.Map<IEnumerable<TeacherGroupsVm>>(
                DbContext.Users.OfType<Teacher>().AsQueryable());
            return teacherGroupsVms.AsQueryable().FirstOrDefault(filterExpression);
        }
        catch (Exception ex) { Logger.LogError(ex, ex.Message); throw; }
    }
}