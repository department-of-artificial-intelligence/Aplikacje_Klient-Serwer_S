using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using DataModels;
using System;
using System.Linq;

namespace SchoolRegister.Web.Controllers
{
    [Authorize]
    public class GradeController : BaseController
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<User> _userManager;

        public GradeController(IGradeService gradeService, IStudentService studentService, ISubjectService subjectService, UserManager<User> userManager, ILogger<GradeController> logger, IMapper mapper, IStringLocalizer<BaseController> localizer)
            : base(logger, mapper, localizer)
        {
            _gradeService = gradeService;
            _studentService = studentService;
            _subjectService = subjectService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult AddGradeToStudent(int studentId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user is not Teacher teacher) return Unauthorized();

            var student = _studentService.GetStudent(s => s.Id == studentId);
            if (student == null) return NotFound();

            // Pobieramy tylko te przedmioty, których uczy ten konkretny nauczyciel
            var subjects = _subjectService.GetSubjects(s => s.TeacherId == teacher.Id);

            ViewBag.SubjectsSelectList = new SelectList(subjects, "Id", "Name");
            ViewBag.GradeValues = new SelectList(Enum.GetValues(typeof(GradeScale)));

            var vm = new AddGradeToStudentVm
            {
                StudentId = studentId,
                TeacherId = teacher.Id
            };

            return View(vm);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGradeToStudent(AddGradeToStudentVm vm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.AddGradeToStudent(vm);
                return RedirectToAction(nameof(StudentGradesReport), new { studentId = vm.StudentId });
            }

            ViewBag.SubjectsSelectList = new SelectList(_subjectService.GetSubjects(s => s.TeacherId == vm.TeacherId), "Id", "Name");
            ViewBag.GradeValues = new SelectList(Enum.GetValues(typeof(GradeScale)));
            return View(vm);
        }

        [Authorize(Roles = "Student, Parent, Teacher, Admin")]
        [HttpGet]
        public IActionResult StudentGradesReport(int? studentId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (studentId.HasValue)
            {
                var getGradesVm = new GetGradesReportVm { StudentId = studentId.Value, GetterUserId = user.Id };
                var report = _gradeService.GetGradesReportForStudent(getGradesVm);
                return View(report);
            }
            if (User.IsInRole("Parent"))
            {
                var children = _studentService.GetStudents(s => s.ParentId == user.Id);

                if (children.Count() == 0) return NotFound("Nie masz przypisanych dzieci.");
                if (children.Count() == 1)
                {
                    var singleChildId = children.First().Id;
                    return RedirectToAction(nameof(StudentGradesReport), new { studentId = singleChildId });
                }
                return View("PickChild", children);
            }
            return RedirectToAction(nameof(StudentGradesReport), new { studentId = user.Id });
        }
    }
}