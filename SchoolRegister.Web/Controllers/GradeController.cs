using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;


namespace SchoolRegister.Web.Controllers
{
    [Authorize]
    public class GradeController : BaseController
    {
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<User> _userManager;

        public GradeController(IGradeService gradeService, ISubjectService subjectService, UserManager<User> userManager,
        ILogger logger, IMapper mapper, IStringLocalizer localizer)
        : base(logger, mapper, localizer)
        {
            _gradeService = gradeService;
            _subjectService = subjectService;
            _userManager = userManager;
        }


        [Authorize(Roles = "Teacher, Admin")]
        [HttpGet]
        public IActionResult AddGrade(int studentId)
        {
            var subjects = _subjectService.GetSubjects();
            ViewBag.SubjectsSelectList = new SelectList(subjects, "Id", "Name");

            return View(new AddGradeToStudentVm { StudentId = studentId });
        }

[HttpPost]
[Authorize(Roles = "Teacher,Admin")] 
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddGrade(AddGradeToStudentVm model)
{
    ModelState.Remove("TeacherId"); 

    if (ModelState.IsValid)
    {
     
        var user = await _userManager.GetUserAsync(User);
        model.TeacherId = user.Id;

        // 3. Zapisujemy
        _gradeService.AddGradeToStudent(model);
        return RedirectToAction("Index", "Student"); 
    }

  
    ViewBag.SubjectsSelectList = new SelectList(_subjectService.GetSubjects(), "Id", "Name");
    return View(model);
}

[Authorize(Roles = "Student,Parent,Teacher,Admin")] 
[HttpGet]

public async Task<IActionResult> MyGrades(int? studentId) 
{
    var user = await _userManager.GetUserAsync(User);
    
    int targetStudentId = studentId ?? 0;

    if (User.IsInRole("Student"))
    {
        targetStudentId = user.Id;
    }

    var reportVm = new GetGradesReportVm 
    { 
        StudentId = targetStudentId,
        GetterUserId = user.Id 
    };
    
    var grades = _gradeService.GetGradesReportForStudent(reportVm);

    return View(grades);
}
    }
}