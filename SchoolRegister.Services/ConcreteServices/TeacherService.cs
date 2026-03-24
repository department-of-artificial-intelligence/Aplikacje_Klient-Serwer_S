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

    public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
    {
        var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
        return Mapper.Map<TeacherVm>(teacher);
    }

    public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterPredicate = null)
    {
        var teachers = filterPredicate == null
            ? DbContext.Users.OfType<Teacher>().ToList()
            : DbContext.Users.OfType<Teacher>().Where(filterPredicate).ToList();
        return Mapper.Map<IEnumerable<TeacherVm>>(teachers);
    }

    public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
    {
        var teacher = DbContext.Users.OfType<Teacher>()
            .FirstOrDefault(t => t.Id == getTeachersGroups.TeacherId);
        if (teacher == null)
            return Enumerable.Empty<GroupVm>();

        var groups = DbContext.Subjects
            .Where(s => s.TeacherId == teacher.Id)
            .SelectMany(s => s.SubjectGroups.Select(sg => sg.Group))
            .Distinct()
            .ToList();

        return Mapper.Map<IEnumerable<GroupVm>>(groups);
    }
}
