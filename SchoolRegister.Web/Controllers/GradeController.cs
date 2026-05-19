using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<User> _userManager;

        public GradeController(IGradeService gradeService, IStudentService studentService, ISubjectService subjectService, UserManager<User> userManager)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null) return Challenge();

            if (_userManager.IsInRoleAsync(user, "Admin").Result)
            {
                return RedirectToAction("Index", "Group");
            }
            else if (_userManager.IsInRoleAsync(user, "Teacher").Result)
            {
                return RedirectToAction("AddGrade");
            }
            else if (_userManager.IsInRoleAsync(user, "Student").Result && user is Student student)
            {
                var reportRequest = new GetGradeReportVm
                {
                    GetterUserId = student.Id,
                    StudentId = student.Id
                };

                var gradeReport = _gradeService.GetGradesReportForStudent(reportRequest);
                return View("GradesReport", gradeReport);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult AddGrade()
        {
            var students = _studentService.GetStudents();
            var subjects = _subjectService.GetSubjects();

            ViewData["Students"] = new SelectList(students.Select(s => new
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName} ({s.GroupName ?? "Brak grupy"})"
            }), "Id", "FullName");

            ViewData["Subjects"] = new SelectList(subjects, "Id", "Name");

            ViewData["GradeValues"] = new SelectList(Enum.GetValues(typeof(GradeScale)));

            return View(new AddGradeToStudentVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade(AddGradeToStudentVm model)
        {
            var currentTeacher = await _userManager.GetUserAsync(User);
            if (currentTeacher != null)
            {
                model.TeacherId = currentTeacher.Id;
            }

            if (ModelState.IsValid)
            {
                _gradeService.AddGradeToStudent(model);
                return RedirectToAction("Index", "Subject");
            }

            var students = _studentService.GetStudents();
            var subjects = _subjectService.GetSubjects();
            ViewData["Students"] = new SelectList(students.Select(s => new { Id = s.Id, FullName = $"{s.FirstName} {s.LastName}" }), "Id", "FullName", model.StudentId);
            ViewData["Subjects"] = new SelectList(subjects, "Id", "Name", model.SubjectId);
            ViewData["GradeValues"] = new SelectList(Enum.GetValues(typeof(GradeScale)), model.GradeValue);

            return View(model);
        }


    }
}