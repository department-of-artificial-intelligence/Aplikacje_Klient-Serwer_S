using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Services.ConcreteServices;

public class GradeService : BaseService, IGradeService
{
    private readonly UserManager<User> _userManager;
    public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }
    public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            var id = addGradeToStudentVm.TeacherId;

            var userEntity = DbContext.Users.OfType<Teacher>()
                .FirstOrDefault(t => t.Id == id);

            if (userEntity == null)
                throw new ArgumentNullException($"Teacher with id: {id} does not exist");

            var isTeacher = _userManager.IsInRoleAsync(userEntity, "Teacher").Result;
            if (!isTeacher)
                throw new UnauthorizedAccessException($"The user with id: {id} doesnt have permission to assign grades");
            
            var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
            DbContext.Add(gradeEntity);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(gradeEntity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesReportVm)
    {
        try
        {
            var studentId = getGradesReportVm.StudentId;
            var getterUserId = getGradesReportVm.GetterUserId;

            var studentEntity = DbContext.Users.OfType<Student>()
                .Include(s => s.Grades)
                    .ThenInclude(g => g.Subject)
                .Include(s => s.Group)
                .FirstOrDefault(s => s.Id == studentId);

            var getterUserEntity = DbContext.Users.OfType<User>()
                .FirstOrDefault(u => u.Id == getterUserId);

            if (studentEntity == null)
                throw new ArgumentNullException($"Student with id: {studentId} does not exist");

            if (getterUserEntity == null)
                throw new ArgumentNullException($"User with id: {getterUserId} does not exist");

            var isTargetStudent = _userManager.IsInRoleAsync(studentEntity, "Student").Result;
            
            if (!isTargetStudent)
                throw new UnauthorizedAccessException($"The student with id: {studentId} doesnt have permission to obtain grades");

            var isGetterTeacher = _userManager.IsInRoleAsync(getterUserEntity, "Teacher").Result;
            var isOwner = studentId == getterUserId;
            var isOwnersParent = studentEntity.ParentId == getterUserId;

            if (!isOwner && !isOwnersParent && !isGetterTeacher)
                throw new UnauthorizedAccessException($"The user with id: {getterUserId} doesnt have permission to see grades");

            return Mapper.Map<GradesReportVm>(studentEntity);
        }
        catch (Exception ex)
        {
           Logger.LogError(ex, ex.Message);
           throw ;
        }
    }
}