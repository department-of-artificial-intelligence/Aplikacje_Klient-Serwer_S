using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;

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

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            var teacher = DbContext.Users
                .OfType<Teacher>()
                .FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);

            if (teacher == null)
                throw new ArgumentException("Teacher not found.");

            var grade = Mapper.Map<Grade>(addGradeToStudentVm);
            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();
            return Mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            var student = DbContext.Users
                .OfType<Student>()
                .FirstOrDefault(s => s.Id == getGradesVm.StudentId);

            if (student == null)
                return new GradesReportVm
                {
                    StudentName = "Student not found",
                    Grades = new List<GradeVm>()
                };

            var grades = student.Grades ?? new List<Grade>();
            var gradesVm = Mapper.Map<List<GradeVm>>(grades);

            return new GradesReportVm
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                Grades = gradesVm
            };
        }
    }
}