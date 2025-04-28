using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly UserManager<User> _userManager;

        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger<GradeService> logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public async Task<GradeVm> AddGradeToStudentAsync(AddGradeToStudentVm addGradeToStudentVm, int teacherUserId)
        {
            var teacher = await DbContext.Users.OfType<Teacher>()
                .FirstOrDefaultAsync(t => t.Id == teacherUserId);

            if (teacher == null || !(await _userManager.IsInRoleAsync(teacher, "Teacher")))
            {
                throw new UnauthorizedAccessException("Tylko nauczyciel może ! TY ANI NU NU :) .");
            }

            var grade = Mapper.Map<Grade>(addGradeToStudentVm);
            DbContext.Grades.Add(grade);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<GradeVm>(grade);
        }

        public async Task<GradesReportVm> GetGradesForStudentAsync(int userId)
        {
            var student = await DbContext.Users.OfType<Student>()
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (student != null)
            {
                var grades = student.Grades ?? new List<Grade>();
                var gradesVm = Mapper.Map<List<GradeVm>>(grades);

                return new GradesReportVm
                {
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Grades = gradesVm
                };
            }

            var parent = await DbContext.Users.OfType<Parent>()
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (parent != null)
            {
                var children = await DbContext.Users.OfType<Student>()
                    .Where(s => s.ParentId == parent.Id)
                    .ToListAsync();

                var allGrades = children.SelectMany(c => c.Grades ?? new List<Grade>());
                var allGradesVm = Mapper.Map<List<GradeVm>>(allGrades);

                return new GradesReportVm
                {
                    StudentName = $"{parent.FirstName} {parent.LastName} (Parent)",
                    Grades = allGradesVm
                };
            }

            throw new UnauthorizedAccessException("Użytkownik nie jest nauczycielem.");
        }
    }
}
