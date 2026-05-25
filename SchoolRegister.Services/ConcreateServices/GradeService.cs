using System;
using System.Linq;
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
                if (addGradeToStudentVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);
                if (teacher == null)
                    throw new InvalidOperationException($"Teacher does not exist");

                var gradeEntity = new Grade
                {
                    DateOfIssue = DateTime.Now,
                    GradeValue = addGradeToStudentVm.GradeValue,
                    StudentId = addGradeToStudentVm.StudentId,
                    SubjectId = addGradeToStudentVm.SubjectId
                };

                DbContext.Grades.Add(gradeEntity);
                DbContext.SaveChanges();

                var gradeVm = Mapper.Map<GradeVm>(gradeEntity);
                return gradeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradesReportVm GetGradesReportForStudent(GetGradeReportVm getGradesVm)
        {
            try
            {
                if (getGradesVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var getter = DbContext.Users.FirstOrDefault(u => u.Id == getGradesVm.GetterUserId);
                if (getter == null)
                    throw new InvalidOperationException($"Getter user does not exist");

                var isTeacher = _userManager.IsInRoleAsync(getter, "Teacher").Result;
                var isParent = _userManager.IsInRoleAsync(getter, "Parent").Result;
                var isStudent = _userManager.IsInRoleAsync(getter, "Student").Result;

                bool canView = isTeacher || 
                               (isStudent && getter.Id == getGradesVm.StudentId) || 
                               (isParent && DbContext.Users.OfType<Student>().Any(s => s.Id == getGradesVm.StudentId && s.ParentId == getter.Id));

                if (!canView)
                    throw new UnauthorizedAccessException($"No permission to view grades");

                var studentEntity = DbContext.Users.OfType<Student>()
                    .Include(s => s.Grades)
                    .Include(s => s.Group)
                    .FirstOrDefault(s => s.Id == getGradesVm.StudentId);

                var reportVm = new GradesReportVm
                {
                    Student = Mapper.Map<StudentVm>(studentEntity),
                    Grades = Mapper.Map<System.Collections.Generic.IEnumerable<GradeVm>>(studentEntity?.Grades)
                };

                return reportVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            throw new NotImplementedException();
        }
    }
}