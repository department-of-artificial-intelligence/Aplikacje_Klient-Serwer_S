using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class GradeService : BaseService, IGradeService
{
    private readonly UserManager<User> _userManager;

    public GradeService(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger logger,
        UserManager<User> userManager)
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            if (addGradeToStudentVm == null)
                throw new ArgumentNullException(nameof(addGradeToStudentVm));

            var teacher = DbContext.Users
                .OfType<Teacher>()
                .FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);

            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            var isTeacher = _userManager
                .IsInRoleAsync(teacher, "Teacher")
                .GetAwaiter()
                .GetResult();

            if (!isTeacher)
                throw new UnauthorizedAccessException("Only teacher can add grade.");

            var student = DbContext.Users
                .OfType<Student>()
                .FirstOrDefault(s => s.Id == addGradeToStudentVm.StudentId);

            if (student == null)
                throw new InvalidOperationException("Student not found.");

            var subject = DbContext.Subjects
                .FirstOrDefault(s => s.Id == addGradeToStudentVm.SubjectId);

            if (subject == null)
                throw new InvalidOperationException("Subject not found.");

            var grade = new Grade
            {
                DateOfIssue = DateTime.Now,
                GradeValue = addGradeToStudentVm.GradeValue,
                StudentId = addGradeToStudentVm.StudentId,
                SubjectId = addGradeToStudentVm.SubjectId,
                Student = student,
                Subject = subject
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
    {
        try
        {
            if (getGradesVm == null)
                throw new ArgumentNullException(nameof(getGradesVm));

            var getterUser = DbContext.Users
                .FirstOrDefault(u => u.Id == getGradesVm.GetterUserId);

            if (getterUser == null)
                throw new InvalidOperationException("Getter user not found.");

            var isTeacher = _userManager
                .IsInRoleAsync(getterUser, "Teacher")
                .GetAwaiter()
                .GetResult();

            var isStudent = _userManager
                .IsInRoleAsync(getterUser, "Student")
                .GetAwaiter()
                .GetResult();

            var isParent = _userManager
                .IsInRoleAsync(getterUser, "Parent")
                .GetAwaiter()
                .GetResult();

            if (!isTeacher && !isStudent && !isParent)
                throw new UnauthorizedAccessException("User has no permission to view grades.");

            if (isStudent && getGradesVm.GetterUserId != getGradesVm.StudentId)
                throw new UnauthorizedAccessException("Student can view only own grades.");

            var student = DbContext.Users
                .OfType<Student>()
                .FirstOrDefault(s => s.Id == getGradesVm.StudentId);

            if (student == null)
                throw new InvalidOperationException("Student not found.");

            var grades = DbContext.Grades
                .Where(g => g.StudentId == getGradesVm.StudentId)
                .OrderByDescending(g => g.DateOfIssue)
                .ToList();

            return new GradesReportVm
            {
                StudentId = student.Id,
                Student = Mapper.Map<StudentVm>(student),
                Grades = Mapper.Map<IList<GradeVm>>(grades)
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}