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
        try
        {
            if (filterPredicate == null)
                throw new ArgumentNullException(nameof(filterPredicate));

            var teacherEntity = DbContext.Users
                .OfType<Teacher>()
                .FirstOrDefault(filterPredicate);

            return Mapper.Map<TeacherVm>(teacherEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<GroupVm> GetTeacherGroups(TeachersGroupsVm getTeachersGroups)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
    {
        try
        {
            var teachers = DbContext.Users
                .OfType<Teacher>()
                .AsQueryable();

            if (filterPredicate != null)
                teachers = teachers.Where(filterPredicate);

            return Mapper.Map<IEnumerable<TeacherVm>>(teachers);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
    {
        if (getTeachersGroups == null)
                throw new ArgumentNullException("VM parameter is null");

            var groups = DbContext.SubjectGroups
                .Where(sg => sg.Subject.TeacherId == getTeachersGroups.TeacherId)
                .Select(sg => sg.Group)
                .Distinct()
                .ToList();

            return Mapper.Map<IEnumerable<GroupVm>>(groups);
    }
}