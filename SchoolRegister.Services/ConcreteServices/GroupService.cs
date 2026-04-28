using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _user_manager;

        public GroupService(ApplicationDbContext db_context, IMapper mapper, ILogger logger, UserManager<User> user_manager)
            : base(db_context, mapper, logger)
        {
            _user_manager = user_manager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm add_or_update_group_vm)
        {
            var group_entity = Mapper.Map<Group>(add_or_update_group_vm);
            if (!add_or_update_group_vm.Id.HasValue || add_or_update_group_vm.Id == 0)
                DbContext.Groups.Add(group_entity);
            else
                DbContext.Groups.Update(group_entity);

            DbContext.SaveChanges();
            return Mapper.Map<GroupVm>(group_entity);
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attach_student_to_group_vm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == attach_student_to_group_vm.StudentId);
            if (student != null)
            {
                student.GroupId = attach_student_to_group_vm.GroupId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attach_subject_group_vm)
        {
            var subject_group = new SubjectGroup { GroupId = attach_subject_group_vm.GroupId, SubjectId = attach_subject_group_vm.SubjectId };
            DbContext.SubjectGroups.Add(subject_group);
            DbContext.SaveChanges();
            return GetGroup(g => g.Id == attach_subject_group_vm.GroupId);
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attach_subject_to_teacher_vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == attach_subject_to_teacher_vm.SubjectId);
            if (subject != null)
            {
                subject.TeacherId = attach_subject_to_teacher_vm.TeacherId;
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detach_student_to_group_vm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == detach_student_to_group_vm.StudentId);
            if (student != null)
            {
                student.GroupId = null;
                DbContext.SaveChanges();
            }
            return Mapper.Map<StudentVm>(student);
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detach_subject_group_vm)
        {
            var subject_group = DbContext.SubjectGroups.FirstOrDefault(sg => sg.GroupId == detach_subject_group_vm.GroupId && sg.SubjectId == detach_subject_group_vm.SubjectId);
            if (subject_group != null)
            {
                DbContext.SubjectGroups.Remove(subject_group);
                DbContext.SaveChanges();
            }
            return GetGroup(g => g.Id == detach_subject_group_vm.GroupId);
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detach_subject_to_teacher_vm)
        {
            var subject = DbContext.Subjects.FirstOrDefault(s => s.Id == detach_subject_to_teacher_vm.SubjectId);
            if (subject != null)
            {
                subject.TeacherId = null;
                DbContext.SaveChanges();
            }
            return Mapper.Map<SubjectVm>(subject);
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filter_predicate)
        {
            var group = DbContext.Groups.Include(g => g.Students).Include(g => g.SubjectGroups).ThenInclude(sg => sg.Subject).FirstOrDefault(filter_predicate);
            return Mapper.Map<GroupVm>(group);
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filter_predicate = null)
        {
            var groups = DbContext.Groups.AsQueryable();
            if (filter_predicate != null) groups = groups.Where(filter_predicate);
            return Mapper.Map<IEnumerable<GroupVm>>(groups);
        }
        public void AddStudentToGroup(int studentId, int groupId)
        {
            var student = DbContext.Users.OfType<SchoolRegister.Model.DataModels.Student>()
                .FirstOrDefault(s => s.Id == studentId);

            if (student != null)
            {
                student.GroupId = groupId;
                DbContext.SaveChanges();
            }
        }

        public void RemoveStudentFromGroup(int studentId, int groupId)
        {
            var student = DbContext.Users.OfType<SchoolRegister.Model.DataModels.Student>()
                .FirstOrDefault(s => s.Id == studentId && s.GroupId == groupId);

            if (student != null)
            {
                student.GroupId = null;
                DbContext.SaveChanges();
            }
        }
    }
}