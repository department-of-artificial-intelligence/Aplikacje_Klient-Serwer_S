using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Services.ConcreteServices;

public class GroupService : BaseService, IGroupService
{
    public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        : base(dbContext, mapper, logger) {}

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        try
        {
            if (addOrUpdateGroupVm == null)
                throw new ArgumentNullException($"View model parameter is null");

            var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);

            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                DbContext.Groups.Add(groupEntity);
            else
                DbContext.Groups.Update(groupEntity);
    
            DbContext.SaveChanges();
            var groupVm = Mapper.Map<GroupVm>(groupEntity);
            return groupVm;
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
            var studentId = attachStudentToGroupVm.StudentId;
            var groupId = attachStudentToGroupVm.GroupId;

            var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == studentId);
            if (studentEntity == null)
                throw new KeyNotFoundException($"Student with id {studentId} not found");
            
            bool groupExists = DbContext.Groups.Any(g => g.Id == groupId);
            if (!groupExists)
                throw new KeyNotFoundException($"Group with id {groupId} not found");
            
            bool studentIsInGroup = studentEntity.GroupId == groupId;
            if (studentIsInGroup)
                throw new InvalidOperationException($"Student with id {studentId} is already in the group with id {groupId}");
            
            studentEntity.GroupId = groupId;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(studentEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectToGroupVm attachSubjectToGroupVm)
    {
       try
        {
            var subjectId = attachSubjectToGroupVm.SubjectId;
            var groupId = attachSubjectToGroupVm.GroupId;

            bool subjectGroupExists = DbContext.SubjectGroups.Any(s => s.SubjectId == subjectId && s.GroupId == groupId);
            if (subjectGroupExists)
                throw new InvalidOperationException($"Subject Group with subject id {subjectId} and group id {groupId} exists");

            bool subjectExists = DbContext.Subjects.Any(s => s.Id == subjectId);

            if (!subjectExists)
                throw new KeyNotFoundException($"Subject with id {subjectId} not found");

            var groupEntity = DbContext.Groups
                .Include(g => g.SubjectGroups)
                    .ThenInclude(sg => sg.Subject)
                .FirstOrDefault(g => g.Id == groupId);
            
            if (groupEntity == null)
                throw new KeyNotFoundException($"Group with id {groupId} not found");
            
            SubjectGroup newSubjectGroupEntity = new SubjectGroup()
            {
                SubjectId = subjectId,
                GroupId = groupId
            };
            
            DbContext.SubjectGroups.Add(newSubjectGroupEntity);
            DbContext.SaveChanges();

            return Mapper.Map<GroupVm>(groupEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachSubjectToTeacherVm)
    {
       try
        {
            var subjectId = attachSubjectToTeacherVm.SubjectId;
            var teacherId = attachSubjectToTeacherVm.TeacherId;

            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == teacherId);
            if (teacherEntity == null)
                throw new KeyNotFoundException($"Teacher with id {teacherId} not found");

            var subjectEntity = DbContext.Subjects
                .Include(s => s.SubjectGroups)
                    .ThenInclude(sg => sg.Group)
                .FirstOrDefault(s => s.Id == subjectId);

            if (subjectEntity == null)
                throw new KeyNotFoundException($"Subject with id {subjectId} not found");
            
            subjectEntity.Teacher = teacherEntity;
            DbContext.SaveChanges();

            return Mapper.Map<SubjectVm>(subjectEntity);
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
            var studentId = detachStudentToGroupVm.StudentId;
            var groupId = detachStudentToGroupVm.GroupId;

            var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == studentId);
            if (studentEntity == null)
                throw new KeyNotFoundException($"Student with id {studentId} not found");
            
            bool studentBelongsToGroup = studentEntity.GroupId == groupId;
            if (!studentBelongsToGroup)
                throw new InvalidOperationException($"Student with id {studentId} does not belong to group with id {groupId}");
            
            studentEntity.GroupId = null;
            DbContext.SaveChanges();
            return Mapper.Map<StudentVm>(studentEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectToGroupVm detachSubjectGroupVm)
    {
       try
        {
            var subjectId = detachSubjectGroupVm.SubjectId;
            var groupId = detachSubjectGroupVm.GroupId;

            var subjectGroupEntity = DbContext.SubjectGroups.FirstOrDefault(sg => sg.SubjectId == subjectId && sg.GroupId == groupId);
            if (subjectGroupEntity == null)
                throw new KeyNotFoundException($"Subject with subject id {subjectId} does not belong to group with id  {groupId}");

            DbContext.SubjectGroups.Remove(subjectGroupEntity);
            DbContext.SaveChanges();
            var groupEntity = DbContext.Groups
                .Include(g => g.SubjectGroups)
                    .ThenInclude(sg => sg.Subject)
                .FirstOrDefault(g => g.Id == groupId);

            if (groupEntity == null)
                throw new KeyNotFoundException($"Group with id {groupId} not found");

            return Mapper.Map<GroupVm>(groupEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm detachSubjectToTeacherVm)
    {
        try
        {
            var subjectId = detachSubjectToTeacherVm.SubjectId;
            var teacherId = detachSubjectToTeacherVm.TeacherId;

            var subjectEntity = DbContext.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subjectEntity == null)
                throw new KeyNotFoundException($"Subject with id {subjectId} not found");
            
            bool teacherBelongsToSubject = subjectEntity.TeacherId == teacherId;
            if (!teacherBelongsToSubject)
                throw new InvalidOperationException($"Teacher with id {teacherId} does not belong to subject with id {subjectId}");
            
            subjectEntity.TeacherId = null;
            DbContext.SaveChanges();
            return Mapper.Map<SubjectVm>(subjectEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterExpression)
    {
        try
        {
            if (filterExpression == null)
                throw new ArgumentNullException("filterExpression is null");
            var groupEntity = DbContext.Groups.FirstOrDefault(filterExpression);
            var groupVm = Mapper.Map<GroupVm>(groupEntity);
            return groupVm;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filterExpression = null)
    {
        try
        {
            var groupEntities = DbContext.Groups.AsQueryable();

            if (filterExpression != null)
                groupEntities = groupEntities.Where(filterExpression);

            var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
            return groupVms;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}