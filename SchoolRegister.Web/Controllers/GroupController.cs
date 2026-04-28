using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Admin, Teacher, Student, Parent")]
public class GroupController : BaseController
{
    private readonly IGroupService group_service;
    private readonly IStudentService student_service;

    public GroupController(
        IGroupService group_service,
        IStudentService student_service,
        ILogger<GroupController> logger,
        IMapper mapper,
        IStringLocalizer<BaseController> localizer) : base(logger, mapper, localizer)
    {
        this.group_service = group_service;
        this.student_service = student_service;
    }

    public IActionResult Index()
    {
        var groups_list = group_service.GetGroups();
        return View(groups_list);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var group_vm = group_service.GetGroup(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateGroupVm>(group_vm));
        }

        ViewBag.ActionType = "Add";
        return View(new AddOrUpdateGroupVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm add_or_update_group_vm)
    {
        if (ModelState.IsValid)
        {
            group_service.AddOrUpdateGroup(add_or_update_group_vm);
            return RedirectToAction("Index");
        }

        return View(add_or_update_group_vm);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AttachStudentToGroup(int student_id, int group_id)
    {
        group_service.AddStudentToGroup(student_id, group_id);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult DetachStudentFromGroup(int student_id, int group_id)
    {
        group_service.RemoveStudentFromGroup(student_id, group_id);
        return RedirectToAction("Index");
    }
    public IActionResult Details(int id)
    {
        var group_vm = group_service.GetGroup(x => x.Id == id);

        var all_students = student_service.GetStudents();

        var students_in_group = all_students.Where(s => s.GroupId == id).ToList();

        var available_students = all_students.Where(s => s.GroupId == null).ToList();

        ViewBag.StudentsInGroup = students_in_group;
        ViewBag.AvailableStudents = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(available_students.Select(s => new {
            Text = $"{s.FirstName} {s.LastName}",
            Value = s.Id
        }), "Value", "Text");

        return View(group_vm);
    }
}