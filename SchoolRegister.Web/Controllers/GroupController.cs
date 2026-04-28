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

[Authorize(Roles = "Admin, Teacher")]
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    private readonly IStudentService _studentService;
    public GroupController(IGroupService groupService, IStudentService studentService, ILogger<GroupController> logger, IMapper mapper, IStringLocalizer<BaseController> localizer) : base(logger, mapper, localizer)
    {
        _groupService = groupService;
        _studentService = studentService;
    }
    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();
        return View(groups);
    }
    [HttpGet]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var groupVm = _groupService.GetGroup(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateGroupVm>(groupVm));
        }
        ViewBag.ActionType = "Add";
        return View(new AddOrUpdateGroupVm());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            return RedirectToAction("Index");
        }
        return View(addOrUpdateGroupVm);
    }
    public IActionResult Details(int id)
    {
        var groupVm = _groupService.GetGroup(x => x.Id == id);
        var allStudents = _studentService.GetStudents();
        var studentsInGroupIds = groupVm.Students.Select(s => s.Id).ToList();
        var availableStudents = allStudents.Where(s => !studentsInGroupIds.Contains(s.Id)).ToList();
        ViewBag.StudentsSelectList = new SelectList(availableStudents.Select(s => new
        {
            Text = $"{s.FirstName} {s.LastName}",
            Value = s.Id
        }), "Value", "Text");
        return View(groupVm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AttachStudentToGroup(AttachDetachStudentToGroupVm model)
    {
        _groupService.AttachStudentToGroup(model);
        return RedirectToAction("Details", new { id = model.GroupId });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DetachStudentFromGroup(AttachDetachStudentToGroupVm model)
    {
        _groupService.DetachStudentFromGroup(model);
        return RedirectToAction("Details", new { id = model.GroupId });
    }
}