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

    public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        var group = Mapper.Map<Group>(addOrUpdateGroupVm);
        if (addOrUpdateGroupVm.Id == null || addOrUpdateGroupVm.Id == 0)
            DbContext.Groups.Add(group);
        else
            DbContext.Groups.Update(group);
        DbContext.SaveChanges();
        return Mapper.Map<GroupVm>(group);
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        var student = DbContext.Users.OfType<Student>()
            .FirstOrDefault(s => s.Id == attachStudentToGroupVm.StudentId);
        if (student == null) throw new Exception("Student not found");
        student.GroupId = attachStudentToGroupVm.GroupId;
        DbContext.SaveChanges();
        return Mapper.Map<StudentVm>(student);
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroup)
    {
        var group = DbContext.Groups.FirstOrDefault(g => g.Id == attachSubjectGroup.GroupId);
        if (group == null) throw new Exception("Group not found");
        var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachSubjectGroup.SubjectId);
        if (subject == null) throw new Exception("Subject not found");
        var subjectGroup = new SubjectGroup { SubjectId = subject.Id, GroupId = group.Id };
        DbContext.SubjectGroups.Add(subjectGroup);
        DbContext.SaveChanges();
        return Mapper.Map<GroupVm>(group);
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
        if (subject == null) throw new Exception("Subject not found");
        subject.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
        DbContext.SaveChanges();
        return Mapper.Map<SubjectVm>(subject);
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
    {
        var student = DbContext.Users.OfType<Student>()
            .FirstOrDefault(s => s.Id == detachStudentToGroupVm.StudentId);
        if (student == null) throw new Exception("Student not found");
        student.GroupId = null;
        DbContext.SaveChanges();
        return Mapper.Map<StudentVm>(student);
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubject)
    {
        var subjectGroup = DbContext.SubjectGroups
            .FirstOrDefault(sg => sg.SubjectId == detachSubject.SubjectId && sg.GroupId == detachSubject.GroupId);
        if (subjectGroup == null) throw new Exception("SubjectGroup not found");
        DbContext.SubjectGroups.Remove(subjectGroup);
        DbContext.SaveChanges();
        var group = DbContext.Groups.FirstOrDefault(g => g.Id == detachSubject.GroupId);
        return Mapper.Map<GroupVm>(group);
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
        if (subject == null) throw new Exception("Subject not found");
        subject.TeacherId = null;
        DbContext.SaveChanges();
        return Mapper.Map<SubjectVm>(subject);
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
    {
        var group = DbContext.Groups.FirstOrDefault(filterPredicate);
        return Mapper.Map<GroupVm>(group);
    }

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterPredicate = null)
    {
        var groups = filterPredicate == null
            ? DbContext.Groups.ToList()
            : DbContext.Groups.Where(filterPredicate).ToList();
        return Mapper.Map<IEnumerable<GroupVm>>(groups);
    }
}
