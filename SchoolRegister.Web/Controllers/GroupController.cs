using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public GroupController(IGroupService groupService, IStudentService studentService, ISubjectService subjectService)
        {
            _groupService = groupService;
            _studentService = studentService;
            _subjectService = subjectService;
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
                var groupVm = _groupService.GetGroup(g => g.Id == id.Value);
                ViewBag.ActionType = "Edit";
                return View(groupVm);
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
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.ActionType = addOrUpdateGroupVm.Id.HasValue ? "Edit" : "Add";
            return View(addOrUpdateGroupVm);
        }

        [HttpGet]
        public IActionResult ManageStudents(int id)
        {
           var group = _groupService.GetGroup(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            var allStudents = _studentService.GetStudents();
            var studentsInGroup = group.Students.Select(s => s.Id).ToList();
            var availableStudents = allStudents.Where(s => !studentsInGroup.Contains(s.Id)).ToList();

            ViewBag.AvailableStudents = new SelectList(availableStudents, "Id", "FirstName"); 
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachStudent(AttachDetachStudentToGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _studentService.AttachStudentToGroup(vm);
            }
            return RedirectToAction(nameof(ManageStudents), new { id = vm.GroupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetachStudent(AttachDetachStudentToGroupVm vm)
        {
            _studentService.DetachStudentFromGroup(vm);
            return RedirectToAction(nameof(ManageStudents), new { id = vm.GroupId });
        }

        [HttpGet]
        public IActionResult AttachSubjectToGroup(int subjectId)
        {
            var vm = new AttachDetachSubjectGroupVm()
            {
                SubjectId = subjectId
            };

            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(groups, "Id", "Name");
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachSubjectToGroup(AttachDetachSubjectGroupVm vm)
        {
            if (ModelState.IsValid)
            {
                _subjectService.AttachSubjectToGroup(vm); 
                return RedirectToAction("Index", "Subject"); 
            }

            var groups = _groupService.GetGroups();
            ViewBag.GroupsSelectList = new SelectList(groups, "Id", "Name");
            return View(vm);
        }

        [HttpGet] 
        public IActionResult DetachSubjectToGroup(int subjectId, int groupId)
        {
            var vm = new AttachDetachSubjectGroupVm()
            {
                SubjectId = subjectId,
                GroupId = groupId
            };

            _subjectService.DetachSubjectFromGroup(vm); 
            return RedirectToAction("Index", "Subject"); 
        }
        
    }
}