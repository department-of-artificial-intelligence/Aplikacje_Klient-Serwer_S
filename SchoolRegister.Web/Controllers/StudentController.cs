using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService, ILogger<StudentController> logger, IMapper mapper, IStringLocalizer<BaseController> localizer)
            : base(logger, mapper, localizer)
        {
            _studentService = studentService;
            _groupService = groupService;
        }
        public IActionResult Index()
        {
            var students = _studentService.GetStudents();
            return View(students);
        }
        [HttpGet]
        public IActionResult AttachToGroup(int studentId)
        {
            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(groups, "Id", "Name");

            var vm = new AttachDetachStudentToGroupVm
            {
                StudentId = studentId
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachToGroup(AttachDetachStudentToGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _groupService.AttachStudentToGroup(vm);
                return RedirectToAction(nameof(Index));
            }

            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(groups, "Id", "Name");
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetachFromGroup(int studentId)
        {
            var vm = new AttachDetachStudentToGroupVm { StudentId = studentId };
            _groupService.DetachStudentFromGroup(vm);

            return RedirectToAction(nameof(Index));
        }
    }
}