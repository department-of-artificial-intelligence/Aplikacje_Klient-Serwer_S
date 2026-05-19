using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Student, Parent")]
    public class StudentGradesController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;

        public StudentGradesController(IGradeService gradeService, IStudentService studentService, UserManager<User> userManager)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (User.IsInRole("Student"))
            {
                var reportRequest = new GetGradeReportVm
                {
                    GetterUserId = currentUser.Id,
                    StudentId = currentUser.Id
                };
                var report = _gradeService.GetGradesReportForStudent(reportRequest);
                return View("GradesReport", report);
            }

            if (User.IsInRole("Parent"))
            {
                var children = _studentService.GetStudents(s => s.ParentId == currentUser.Id);
                return View("ChildrenList", children);
            }

            return Forbid();
        }

        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> ViewChildGrades(int studentId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var reportRequest = new GetGradeReportVm
            {
                GetterUserId = currentUser.Id,
                StudentId = studentId
            };

            try
            {
                var report = _gradeService.GetGradesReportForStudent(reportRequest);
                return View("GradesReport", report);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}