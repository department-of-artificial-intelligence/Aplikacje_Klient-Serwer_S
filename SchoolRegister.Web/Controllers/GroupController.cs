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
public class GroupController : BaseController
{
    private readonly ISubjectService _subjectService;
    private readonly IStudentService _studentService;
    private readonly IGroupService _groupService;

    private readonly UserManager<User> _userManager;
    public GroupController(ISubjectService subjectService,
    IStudentService studentService,
    IGroupService groupService,
    UserManager<User> userManager,
    IStringLocalizer localizer,
    ILogger logger,
    IMapper mapper) : base(logger, mapper, localizer)
    {
        _subjectService = subjectService;
        _groupService = groupService;
        _studentService = studentService;

        _userManager = userManager;
    }
    public IActionResult Index()
    {
        var user = _userManager.GetUserAsync(User).Result;
        if (_userManager.IsInRoleAsync(user, "Admin").Result)
            return View(_groupService.GetGroups());
        else
            return View("Error");
    }
    public IActionResult Details(int id)
    {
        var groupVm = _groupService.GetGroup(x => x.Id == id);
        return View(groupVm);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var groupVm = _groupService.GetGroup(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateGroupVm>(groupVm));
        }
        ViewBag.ActionType = "Add";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubjectToGroup(int subjectId)
    {
        var groupsVm = _groupService
            .GetGroups(g => !g.SubjectGroups
                .Any(sg => sg.SubjectId == subjectId)
            );
        ViewBag.GroupSelectList = new SelectList(groupsVm.Select(g => new
        {
            Text = $"{g.Name}",
            Value = g.Id
        }), "Value", "Text");
        ViewBag.ActionType = "Add";
        var vm = new AttachDetachSubjectGroupVm()
        {
            SubjectId = subjectId
        };
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectToGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AttachSubjectToGroup(attachSubjectToGroupVm);
            return RedirectToAction("Index", "Subject");
        }
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubjectFromGroup(int subjectId)
    {
        var groupsVm = _groupService
            .GetGroups(g => g.SubjectGroups
                .Any(sg => sg.SubjectId == subjectId)
            );

        if (groupsVm == null || !groupsVm.Any())
        {
            TempData["ErrorMessage"] = "Ten przedmiot nie jest obecnie przypisany do żadnej grupy.";
            return RedirectToAction("Index", "Subject");
        }
        ViewBag.GroupSelectList = new SelectList(groupsVm.Select(g => new
        {
            Text = $"{g.Name}",
            Value = g.Id
        }), "Value", "Text");
        ViewBag.ActionType = "Add";
        var vm = new AttachDetachSubjectGroupVm()
        {
            SubjectId = subjectId
        };
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectFromGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.DetachSubjectFromGroup(detachSubjectFromGroupVm);
            return RedirectToAction("Index", "Subject");
        }
        return View();
    }


    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudentToGroup(int studentId)
    {
        var groupsVm = _groupService
            .GetGroups(g => !g.Students
                .Any(s => s.Id == studentId) 
            );

        ViewBag.GroupSelectList = new SelectList(groupsVm.Select(g => new
        {
            Text = $"{g.Name}",
            Value = g.Id
        }), "Value", "Text");
        ViewBag.ActionType = "Add";
        var vm = new AttachDetachStudentToGroupVm()
        {
            StudentId = studentId
        };
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AttachStudentToGroup(attachStudentToGroupVm);
            return RedirectToAction("Index", "Student");
        }
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudentFromGroup(int studentId)
    {
        var groupVm = _groupService
            .GetGroup(g => g.Students
                .Any(st => st.Id == studentId)
            );

        if (groupVm == null)
        {
            TempData["ErrorMessage"] = "Wybrany student nie jest obecnie przypisany do żadnej grupy.";
            return RedirectToAction("Index", "Student");
        }

        var vm = new AttachDetachStudentToGroupVm()
        {
            GroupId = groupVm.Id,
            StudentId = studentId
        };
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentFromGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.DetachStudentFromGroup(detachStudentFromGroupVm);
            return RedirectToAction("Index", "Student");
        }
        return View();
    }
}