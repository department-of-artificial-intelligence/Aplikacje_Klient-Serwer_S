using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher,Student,Parent")]
public class GradeController : Controller
{
    private readonly IGradeService _gradeService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;
    private readonly UserManager<User> _userManager;

    public GradeController(
        IGradeService gradeService,
        IStudentService studentService,
        ISubjectService subjectService,
        UserManager<User> userManager)
    {
        _gradeService = gradeService;
        _studentService = studentService;
        _subjectService = subjectService;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    public IActionResult AddGrade()
    {
        ViewBag.Students = new SelectList(_studentService.GetStudents(), "Id", "FirstName");
        ViewBag.Subjects = new SelectList(_subjectService.GetSubjects(), "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public IActionResult AddGrade(AddGradeToStudentVm vm)
    {
        if (ModelState.IsValid)
        {
            _gradeService.AddGradeToStudent(vm);
            return RedirectToAction("MyGrades");
        }

        ViewBag.Students = new SelectList(_studentService.GetStudents(), "Id", "FirstName");
        ViewBag.Subjects = new SelectList(_subjectService.GetSubjects(), "Id", "Name");

        return View(vm);
    }

    public IActionResult MyGrades()
    {
        var user = _userManager.GetUserAsync(User).Result;

        if (user is Student student)
        {
            var vm = new GetGradesReportVm
            {
                StudentId = student.Id
            };

            var grades = _gradeService.GetGradesReportForStudent(vm);
            return View(grades);
        }

        if (user is Parent parent)
        {
            var child = _studentService.GetStudents(s => s.ParentId == parent.Id).FirstOrDefault();

            if (child != null)
            {
                var vm = new GetGradesReportVm
                {
                    StudentId = child.Id
                };

                var grades = _gradeService.GetGradesReportForStudent(vm);
                return View(grades);
            }
        }

        return View("Error");
    }
}