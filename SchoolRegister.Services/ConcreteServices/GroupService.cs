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

public class GroupService : BaseService, IGroupService
{
    private readonly UserManager<User> _userManager;

    public GroupService(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger logger,
        UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        try
        {
            if (addOrUpdateGroupVm == null)
                throw new ArgumentNullException(nameof(addOrUpdateGroupVm));

            Group groupEntity;

            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
            {
                groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);
                DbContext.Groups.Add(groupEntity);
            }
            else
            {
                groupEntity = DbContext.Groups.FirstOrDefault(g => g.Id == addOrUpdateGroupVm.Id.Value);
                if (groupEntity == null)
                    throw new InvalidOperationException("Group not found.");

                groupEntity.Name = addOrUpdateGroupVm.Name;
                DbContext.Groups.Update(groupEntity);
            }

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(groupEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        try
        {
            if (attachStudentToGroupVm == null)
                throw new ArgumentNullException(nameof(attachStudentToGroupVm));

            var group = DbContext.Groups.FirstOrDefault(g => g.Id == attachStudentToGroupVm.GroupId);
            if (group == null)
                throw new InvalidOperationException("Group not found.");

            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);
            if (student == null)
                throw new InvalidOperationException("Student not found.");

            student.GroupId = group.Id;
            student.Group = group;

            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectToGroup)
    {
        try
        {
            if (attachSubjectToGroup == null)
                throw new ArgumentNullException(nameof(attachSubjectToGroup));

            var group = DbContext.Groups.FirstOrDefault(g => g.Id == attachSubjectToGroup.GroupId);
            if (group == null)
                throw new InvalidOperationException("Group not found.");

            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachSubjectToGroup.SubjectId);
            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            var exists = DbContext.SubjectGroups.Any(sg =>
                sg.GroupId == group.Id && sg.SubjectId == subject.Id);

            if (!exists)
            {
                DbContext.SubjectGroups.Add(new SubjectGroup
                {
                    GroupId = group.Id,
                    SubjectId = subject.Id
                });
                DbContext.SaveChanges();
            }

            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        try
        {
            if (attachDetachSubjectToTeacherVm == null)
                throw new ArgumentNullException(nameof(attachDetachSubjectToTeacherVm));

            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            var teacher = DbContext.Users.OfType<Teacher>()
                .FirstOrDefault(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            subject.TeacherId = teacher.Id;
            subject.Teacher = teacher;

            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
    {
        try
        {
            if (detachStudentToGroupVm == null)
                throw new ArgumentNullException(nameof(detachStudentToGroupVm));

            var student = DbContext.Users.OfType<Student>()
                .FirstOrDefault(s => s.Id == detachStudentToGroupVm.StudentId);
            if (student == null)
                throw new InvalidOperationException("Student not found.");

            student.GroupId = null;
            student.Group = null;

            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(student);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachDetachSubject)
    {
        try
        {
            if (detachDetachSubject == null)
                throw new ArgumentNullException(nameof(detachDetachSubject));

            var link = DbContext.SubjectGroups.FirstOrDefault(sg =>
                sg.GroupId == detachDetachSubject.GroupId &&
                sg.SubjectId == detachDetachSubject.SubjectId);

            if (link != null)
            {
                DbContext.SubjectGroups.Remove(link);
                DbContext.SaveChanges();
            }

            var group = DbContext.Groups.FirstOrDefault(g => g.Id == detachDetachSubject.GroupId);
            return Mapper.Map<GroupVm>(group);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        try
        {
            if (attachDetachSubjectToTeacherVm == null)
                throw new ArgumentNullException(nameof(attachDetachSubjectToTeacherVm));

            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            subject.TeacherId = null;
            subject.Teacher = null;

            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subject);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
    {
        try
        {
            if (filterPredicate == null)
                throw new ArgumentNullException(nameof(filterPredicate));

            var groupEntity = DbContext.Groups.FirstOrDefault(filterPredicate);
            return Mapper.Map<GroupVm>(groupEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
    {
        try
        {
            var groups = DbContext.Groups.AsQueryable();

            if (filterPredicate != null)
                groups = groups.Where(filterPredicate);

            return Mapper.Map<IEnumerable<GroupVm>>(groups.ToList());
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}