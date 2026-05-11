using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Student, Parent")]
public class GradeController : BaseController
{
    private readonly IGradeService _gradeService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;
    private readonly UserManager<User> _userManager;

    public GradeController(IGradeService gradeService,
        IStudentService studentService,
        ISubjectService subjectService,
        UserManager<User> userManager,
        IStringLocalizer localizer,
        ILogger logger,
        IMapper mapper) : base(logger, mapper, localizer)
    {
        _gradeService = gradeService;
        _studentService = studentService;
        _subjectService = subjectService;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult AddGrade(int studentId)
    {
        ViewBag.StudentId = studentId;
        ViewBag.Subjects = _subjectService.GetSubjects();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult AddGrade(AddGradeToStudentVm addGradeToStudentVm)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.GetUserAsync(User).Result;
            addGradeToStudentVm.TeacherId = user.Id;
            _gradeService.AddGradeToStudent(addGradeToStudentVm);
            return RedirectToAction("Index", "Student");
        }
        return View();
    }

    [HttpGet]
    public IActionResult GradesReport(int studentId)
    {
        var user = _userManager.GetUserAsync(User).Result;
        var getGradesReportVm = new GetGradesReportVm
        {
            StudentId = studentId,
            GetterUserId = user.Id
        };
        var report = _gradeService.GetGradesReportForStudent(getGradesReportVm);
        return View(report);
    }
}