using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SchoolRegister.Services.ConcreteServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolRegister.Services.ConcreteServices;
public class GroupService : BaseService, IGroupService
{
    private readonly UserManager<User> _userManager;

    public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) 
        : base(dbContext, mapper, logger) { _userManager = userManager; }

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm vm)
    {
        var group = vm.Id.HasValue ? DbContext.Groups.Find(vm.Id.Value) : new Group();
        group.Name = vm.Name;
        if (!vm.Id.HasValue) DbContext.Groups.Add(group);
        DbContext.SaveChanges();
        return Mapper.Map<GroupVm>(group);
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
    {
        var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId);
        var group = DbContext.Groups.Find(vm.GroupId);
        if (student == null || group == null) return null!;
        student.GroupId = group.Id;
        DbContext.SaveChanges();
        return Mapper.Map<StudentVm>(student);
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
    {
        var subjectGroup = new SubjectGroup { SubjectId = vm.SubjectId, GroupId = vm.GroupId };
        DbContext.SubjectGroups.Add(subjectGroup);
        DbContext.SaveChanges();
        return Mapper.Map<GroupVm>(DbContext.Groups.Find(vm.GroupId));
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm vm)
    {
        var subject = DbContext.Subjects.Find(vm.SubjectId);
        var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == vm.TeacherId);
        if (subject == null || teacher == null) return null!;
        subject.TeacherId = teacher.Id;
        DbContext.SaveChanges();
        return Mapper.Map<SubjectVm>(subject);
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
    {
        var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == vm.StudentId);
        if (student == null) return null!;
        student.GroupId = null;
        DbContext.SaveChanges();
        return Mapper.Map<StudentVm>(student);
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
    {
        var sg = DbContext.SubjectGroups.FirstOrDefault(x => x.SubjectId == vm.SubjectId && x.GroupId == vm.GroupId);
        if (sg != null) { DbContext.SubjectGroups.Remove(sg); DbContext.SaveChanges(); }
        return Mapper.Map<GroupVm>(DbContext.Groups.Find(vm.GroupId));
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm vm)
    {
        var subject = DbContext.Subjects.Find(vm.SubjectId);
        if (subject != null) { subject.TeacherId = null; DbContext.SaveChanges(); }
        return Mapper.Map<SubjectVm>(subject);
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filter) => Mapper.Map<GroupVm>(DbContext.Groups.FirstOrDefault(filter));

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filter = null!) 
    {
        var query = DbContext.Groups.AsQueryable();
        if (filter != null) query = query.Where(filter);
        return Mapper.Map<IEnumerable<GroupVm>>(query.ToList());
    }
}