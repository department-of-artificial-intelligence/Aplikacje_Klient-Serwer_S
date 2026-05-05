using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Student")]
public class GradeController : BaseController
{
    private readonly IGradeService _gradeService;
    private readonly ISubjectService _subjectService;
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;

    public GradeController(IGradeService gradeService,
    ISubjectService subjectService,
    IStudentService studentService,
    UserManager<User> userManager,
    IStringLocalizer localizer,
    ILogger logger,
    IMapper mapper) : base(logger, mapper, localizer)
    {
        _gradeService = gradeService;
        _subjectService = subjectService;
        _studentService = studentService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;
        if (_userManager.IsInRoleAsync(user, "Admin").Result)
            return View(_gradeService.);
        else if (_userManager.IsInRoleAsync(user, "Teacher").Result && user is Teacher teacher)
        {
            return View(_subjectService.GetSubjects(x => x.TeacherId == teacher.Id));
        }
        else if (_userManager.IsInRoleAsync(user, "Student").Result)
            return RedirectToAction("Details", "Student", new { studentId = user.Id });
        else
            return View("Error");
    }

    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult AddGrade(int? id = null)
    {
        if (!id.HasValue)
        {
            return RedirectToAction("Index", "Student");
        }

        var student = _studentService.GetStudent(s => s.Id == id);
        if (student == null) return View("Error");

        var user = _userManager.GetUserAsync(User).Result;
        IEnumerable<SubjectVm> subjectsVm;

        if (_userManager.IsInRoleAsync(user, "Admin").Result)
            subjectsVm = _subjectService.GetSubjects();
        else
            subjectsVm = _subjectService.GetSubjects(x => x.TeacherId == user.Id);

        ViewBag.SubjectsSelectList = new SelectList(subjectsVm, "Id", "Name");


        ViewBag.GradeScalesSelectList = new SelectList(Enum.GetValues(typeof(GradeScale))
            .Cast<GradeScale>()
            .Select(gv => new
            {
                Value = (int)gv,
                Text = gv.ToString()
            }), "Value", "Text");

        var model = new AddGradeToStudentVm
        {
            StudentId = student.Id,
            TeacherId = user.Id
        };

        return View(model);
    }

}

