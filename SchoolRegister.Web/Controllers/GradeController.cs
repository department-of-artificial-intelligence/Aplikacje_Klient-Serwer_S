using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher")]
public class GradeController : BaseController
{
    private readonly IGradeService grade_service;
    private readonly ISubjectService subject_service;
    private readonly IStudentService student_service;

    public GradeController(
        IGradeService grade_service,
        ISubjectService subject_service,
        IStudentService student_service,
        ILogger<GradeController> logger,
        IMapper mapper,
        IStringLocalizer<BaseController> localizer) : base(logger, mapper, localizer)
    {
        this.grade_service = grade_service;
        this.subject_service = subject_service;
        this.student_service = student_service;
    }

    [HttpGet]
    public IActionResult AddGrade()
    {
        var subjects = subject_service.GetSubjects();
        var students = student_service.GetStudents();

        ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
        ViewBag.Students = new SelectList(students.Select(s => new {
            Id = s.Id,
            FullName = $"{s.FirstName} {s.LastName}"
        }), "Id", "FullName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddGrade(AddGradeToStudentVm add_grade_vm)
    {
        if (ModelState.IsValid)
        {
            grade_service.AddGradeToStudent(add_grade_vm);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Subjects = new SelectList(subject_service.GetSubjects(), "Id", "Name");
        ViewBag.Students = new SelectList(student_service.GetStudents().Select(s => new {
            Id = s.Id,
            FullName = $"{s.FirstName} {s.LastName}"
        }), "Id", "FullName");

        return View(add_grade_vm);
    }
}