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
public class SubjectController : BaseController
{
    private readonly ISubjectService _subjectService;
    private readonly ITeacherService _teacherService;
    private readonly IGroupService _groupService;
    private readonly UserManager<User> _userManager;

    public SubjectController(
        ISubjectService subjectService,
        ITeacherService teacherService,
        IGroupService groupService,
        UserManager<User> userManager,
        IStringLocalizer localizer,
        ILogger logger,
        IMapper mapper
    ) : base(logger, mapper, localizer)
    {
        _subjectService = subjectService;
        _teacherService = teacherService;
        _groupService = groupService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;

        ViewBag.Groups = _groupService.GetGroups();

        if (_userManager.IsInRoleAsync(user, "Admin").Result)
            return View(_subjectService.GetSubjects());

        else if (_userManager.IsInRoleAsync(user, "Teacher").Result && user is Teacher teacher)
            return View(_subjectService.GetSubjects(x => x.TeacherId == teacher.Id));

        else if (_userManager.IsInRoleAsync(user, "Student").Result)
            return RedirectToAction("Details", "Student", new { studentId = user.Id });

        else
            return View("Error");
    }

    // ADD / EDIT

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditSubject(int? id = null)
    {
        var teachersVm = _teacherService.GetTeachers();

        ViewBag.TeachersSelectList = new SelectList(teachersVm.Select(t => new
        {
            Text = $"{t.FirstName} {t.LastName}",
            Value = t.Id
        }), "Value", "Text");

        if (id.HasValue)
        {
            var subjectVm = _subjectService.GetSubject(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateSubjectVm>(subjectVm));
        }

        ViewBag.ActionType = "Add";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditSubject(AddOrUpdateSubjectVm vm)
    {
        if (ModelState.IsValid)
        {
            _subjectService.AddOrUpdateSubject(vm);
            return RedirectToAction("Index");
        }

        return View(vm);
    }

    // DETAILS

    public IActionResult Details(int id)
    {
        var subjectVm = _subjectService.GetSubject(x => x.Id == id);

        if (subjectVm == null)
            return NotFound();

        return View(subjectVm);
    }

    // ATTACH / DETACH GROUP

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubjectToGroup(int subjectId, int groupId)
    {
        _groupService.AttachSubjectToGroup(new AttachDetachSubjectGroupVm
        {
            SubjectId = subjectId,
            GroupId = groupId
        });

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubjectFromGroup(int subjectId, int groupId)
    {
        _groupService.DetachSubjectFromGroup(new AttachDetachSubjectGroupVm
        {
            SubjectId = subjectId,
            GroupId = groupId
        });

        return RedirectToAction("Index");
    }
}