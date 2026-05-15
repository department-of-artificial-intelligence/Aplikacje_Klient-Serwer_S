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

[Authorize(Roles = "Teacher, Admin, Student, Parent")]
public class GradeController : BaseController
{
    private readonly IGradeService _gradeService;
    private readonly ISubjectService _subjectService;
    private readonly UserManager<User> _userManager;
    public GradeController(IGradeService gradeService,
    ISubjectService subjectService,
    UserManager<User> userManager,
    IStringLocalizer localizer,
    ILogger logger,
    IMapper mapper) : base(logger, mapper, localizer)
    {
        _gradeService = gradeService;
        _subjectService = subjectService;
        _userManager = userManager;
    }
    //TODO: zrobic redisegn index, zeby zwracal caly viewmodel, a widok mogl wyswietlac imie/nazwisko studenta i srednia ocen
    public IActionResult Index(int? studentId)
    {
        var user = _userManager.GetUserAsync(User).Result;
        int targetStudentId;

        if (studentId.HasValue) {
            targetStudentId = studentId.Value;
        }
        else if (_userManager.IsInRoleAsync(user, "Student").Result)
        {
            targetStudentId = user.Id;
        }
        else
        {
            return View("Error");
        }
        var gradesReportVm = _gradeService.GetGradesReportForStudent(new GetGradesReportVm
            {
                StudentId = targetStudentId,
                GetterUserId = user.Id
            });

        return View(gradesReportVm.Grades);
    }
    [HttpGet]
    [Authorize(Roles = "Teacher, Admin")]

    public IActionResult AddGradeToStudent(int studentId)
    {
        var user = _userManager.GetUserAsync(User).Result;

        bool isAdmin = !_userManager.IsInRoleAsync(user, "Admin").Result;
        bool isTeacher = !_userManager.IsInRoleAsync(user, "Teacher").Result;

        if (!isTeacher && !isAdmin)
            return View("Error");
        
        var subjectsVm = _subjectService
            .GetSubjects(su => su.SubjectGroups
                .Any(sg => sg.Group.Students
                    .Any(st => st.Id == studentId)
                )
            );

        ViewBag.SubjectsSelectList = new SelectList(subjectsVm, "Id", "Name");

        var model = new AddGradeToStudentVm
        {
            StudentId = studentId,
            TeacherId = user.Id
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
    {
        if (ModelState.IsValid)
        {
            _gradeService.AddGradeToStudent(addGradeToStudentVm);
            return RedirectToAction("Index", "Grade", new { studentId = addGradeToStudentVm.StudentId });
        }
        return View();
    }
}
