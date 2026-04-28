using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly UserManager<User> _user_manager;

        public GradeService(ApplicationDbContext db_context, IMapper mapper, ILogger logger, UserManager<User> user_manager)
            : base(db_context, mapper, logger)
        {
            _user_manager = user_manager;
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm add_grade_to_student_vm)
        {
            var grade = new Grade
            {
                StudentId = add_grade_to_student_vm.StudentId,
                SubjectId = add_grade_to_student_vm.SubjectId,
                GradeValue = add_grade_to_student_vm.GradeValue,
                DateOfIssue = DateTime.Now
            };

            DbContext.Grades.Add(grade);
            DbContext.SaveChanges();

            return new GradeVm();
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm get_grades_vm)
        {
            return new GradesReportVm();
        }
    }
}