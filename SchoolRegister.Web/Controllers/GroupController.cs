using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student, Parent")]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService, ILogger<GroupController> logger, IMapper mapper, IStringLocalizer<BaseController> localizer) : base(logger, mapper, localizer)
        {
            _groupService = groupService;
        }

        public IActionResult Index()
        {
            var groups = _groupService.GetGroups();
            return View(groups);
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
                return RedirectToAction(nameof(Index));
            }
            return View(addOrUpdateGroupVm);
        }
        public IActionResult Details(int id)
        {
            var groupVm = _groupService.GetGroup(x => x.Id == id);
            if (groupVm == null) return NotFound();

            return View(groupVm);
        }
    }
}
