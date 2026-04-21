using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

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
            var grade = new Grade
            {
                DateOfIssue = DateTime.Now,
                SubjectId = addGradeToStudentVm.SubjectId,
                StudentId = addGradeToStudentVm.StudentId,
                GradeValue = addGradeToStudentVm.GradeValue
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return Mapper.Map<GradeVm>(grade);
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            var student = DbContext.Users.OfType<Student>().FirstOrDefault(s => s.Id == getGradesVm.StudentId);
            if (student == null) return null;

            var report = new GradesReportVm
            {
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName,
                GroupName = student.Group?.Name,
                ParentName = student.Parent != null ? $"{student.Parent.FirstName} {student.Parent.LastName}" : null,
                StudentAverageGradePerSubject = student.AverageGradePerSubject,
                StudentGradesPerSubject = student.GradesPerSubject
            };

            return report;
        }
    }
}