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
    public IActionResult AddOrEditSubject(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AttachSubjectToGroup(AttachDetachSubjectToGroupVm attachSubjectToGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AttachSubjectToGroup(attachSubjectToGroupVm);
            return RedirectToAction("Index", "Subject");
        }
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DetachSubjectFromGroup(AttachDetachSubjectToGroupVm detachSubjectFromGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.DetachSubjectFromGroup(detachSubjectFromGroupVm);
            return RedirectToAction("Index", "Subject");
        }
        return View();
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
