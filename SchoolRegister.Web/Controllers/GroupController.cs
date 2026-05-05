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
    [Authorize(Roles = "Teacher, Admin")]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<User> _userManager;

        public GroupController(
            IGroupService groupService,
            IStudentService studentService,
            ISubjectService subjectService,
            UserManager<User> userManager,
            IStringLocalizer localizer,
            ILogger logger,
            IMapper mapper)
            : base(logger, mapper, localizer)
        {
            _groupService = groupService;
            _studentService = studentService;
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_groupService.GetGroups());
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
                ViewBag.ActionType = "Edit";
                var groupVm = _groupService.GetGroup(x => x.Id == id);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachStudentToGroup(int groupId)
        {
            var students = _studentService.GetStudents(s => s.GroupId == null || s.GroupId != groupId);
            ViewBag.StudentsSelectList = new SelectList(
                students.Select(s => new { Text = s.FirstName + " " + s.LastName, Value = s.Id }),
                "Value", "Text");

            return View(new AttachDetachStudentToGroupVm { GroupId = groupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachStudentToGroup(AttachDetachStudentToGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _groupService.AttachStudentToGroup(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachStudentFromGroup(int groupId)
        {
            var group = _groupService.GetGroup(g => g.Id == groupId);
            ViewBag.StudentsSelectList = new SelectList(
                group?.Students?.Select(s => new { Text = s.FirstName + " " + s.LastName, Value = s.Id })
                ?? Enumerable.Empty<object>(),
                "Value", "Text");

            return View(new AttachDetachStudentToGroupVm { GroupId = groupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachStudentFromGroup(AttachDetachStudentToGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _groupService.DetachStudentFromGroup(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachSubjectToGroup(int subjectId)
        {
            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(
                groups.Select(g => new { Text = g.Name, Value = g.Id }),
                "Value", "Text");

            return View(new AttachDetachSubjectGroupVm { SubjectId = subjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _groupService.AttachSubjectToGroup(vm);
                return RedirectToAction("Index", "Subject");
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachSubjectFromGroup(int subjectId)
        {
            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(
                groups.Select(g => new { Text = g.Name, Value = g.Id }),
                "Value", "Text");

            return View(new AttachDetachSubjectGroupVm { SubjectId = subjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachSubjectFromGroup(AttachDetachSubjectGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _groupService.DetachSubjectFromGroup(vm);
                return RedirectToAction("Index", "Subject");
            }
            return View(vm);
        }
    }
}