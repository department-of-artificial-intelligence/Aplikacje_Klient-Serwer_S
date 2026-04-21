using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (teacher == null) return null;


            var isTeacher = _userManager.IsInRoleAsync(teacher, "Teacher").GetAwaiter().GetResult();
            if (!isTeacher) return null;


            var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
            gradeEntity.DateOfIssue = DateTime.Now;

            DbContext.Grades.Add(gradeEntity);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(gradeEntity);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {

            var student = DbContext.Users.OfType<Student>()
            .FirstOrDefault(s => s.Id == getGradesVm.StudentId);

            if (student == null) return null;


            var studentGrades = DbContext.Grades
            .Where(g => g.StudentId == student.Id)
            .ToList();


            return new GradesReportVm()
            {
                StudentFullName = $"{student.FirstName} {student.LastName}",
                GroupName = student.Group?.Name ?? "Brak grupy",
                Grades = Mapper.Map<IEnumerable<GradeVm>>(studentGrades),
                AverageGrade = studentGrades.Any() ? studentGrades.Average(g => (int)g.GradeValue) : 0
            };
        }
    }
}