using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var teacher = DbContext.Users.OfType<Teacher>()
                .FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);

            if (teacher == null || !_userManager.IsInRoleAsync(teacher, "Teacher").Result)
                throw new Exception("Nieprawidłowy nauczyciel.");

            var grade = new Grade
            {
                GradeValue = addGradeToStudentVm.GradeValue,
                DateOfIssue = DateTime.Now,
                StudentId = addGradeToStudentVm.StudentId,
                SubjectId = addGradeToStudentVm.SubjectId
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesReportVm)
        {
            var student = DbContext.Users.OfType<Student>()
                .Include(s => s.Grades)
                .ThenInclude(g => g.Subject)
                .FirstOrDefault(s => s.Id == getGradesReportVm.StudentId);

            if (student == null) throw new Exception("Nie znaleziono studenta.");

            var user = DbContext.Users.FirstOrDefault(u => u.Id == getGradesReportVm.GetterUserId);

            if (user == null)
                throw new Exception("Nie znaleziono użytkownika.");

            var isStudent = _userManager.IsInRoleAsync(user, "Student").Result;
            var isParent = _userManager.IsInRoleAsync(user, "Parent").Result;
            var isTeacher = _userManager.IsInRoleAsync(user, "Teacher").Result;

            if (!(isTeacher || (isStudent && student.Id == user.Id) || (isParent && student.ParentId == user.Id)))
                throw new Exception("Brak uprawnień.");

            return Mapper.Map<GradesReportVm>(student);
        }
    }
}
