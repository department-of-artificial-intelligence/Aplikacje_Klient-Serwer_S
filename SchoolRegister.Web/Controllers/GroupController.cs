using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers;

[Authorize(Roles = "Teacher, Admin, Student")]
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(
        IGroupService groupService,
        IStringLocalizer localizer,
        ILogger logger,
        IMapper mapper) : base(logger, mapper, localizer)
    {
        _groupService = groupService;
    }

    public IActionResult Index()
    {
        return View(_groupService.GetGroups());
    }

    public IActionResult Details(int id)
    {
        var groupVm = _groupService.GetGroup(x => x.Id == id);
        if (groupVm == null)
            return View("Error");

        return View(groupVm);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(int? id = null)
    {
        if (id.HasValue)
        {
            var groupVm = _groupService.GetGroup(x => x.Id == id.Value);
            if (groupVm == null)
                return View("Error");

            ViewBag.ActionType = "Edit";
            return View(new AddOrUpdateGroupVm
            {
                Id = groupVm.Id,
                Name = groupVm.Name
            });
        }

        ViewBag.ActionType = "Add";
        return View(new AddOrUpdateGroupVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditGroup(AddOrUpdateGroupVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ActionType = vm.Id == 0 ? "Add" : "Edit";
            return View(vm);
        }

        _groupService.AddOrUpdateGroup(vm);
        return RedirectToAction(nameof(Index));
    }
}