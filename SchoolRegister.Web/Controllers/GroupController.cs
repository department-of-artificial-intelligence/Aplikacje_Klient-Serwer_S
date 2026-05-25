using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
            return View(groups ?? new List<GroupVm>());
        }

        [HttpGet]
        public IActionResult AddOrEditGroup(int? id)
        {
            if (id == null)
            {
                ViewBag.ActionType = "Dodaj";
                return View(new AddOrUpdateGroupVm());
            }

            ViewBag.ActionType = "Edytuj";
            var groupVm = _groupService.GetGroup(g => g.Id == id.Value);
            if (groupVm == null) return NotFound();

            var modelForView = new AddOrUpdateGroupVm
            {
                Id = groupVm.Id,
                Name = groupVm.Name
            };

            return View(modelForView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEditGroup(AddOrUpdateGroupVm model)
        {
            if (ModelState.IsValid)
            {
                _groupService.AddOrUpdateGroup(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ActionType = (!model.Id.HasValue || model.Id == 0) ? "Dodaj" : "Edytuj";
            return View(model);
        }

        [HttpGet]
        public IActionResult ManageGroupStudents(int id)
        {
            var group = _groupService.GetGroup(g => g.Id == id);
            if (group == null) return NotFound();

            ViewBag.Group = group;

            var availableStudents = _studentService.GetStudents(s => s.GroupId == null);

            ViewData["AvailableStudents"] = new SelectList((availableStudents ?? Enumerable.Empty<StudentVm>()).Select(s => new
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName} ({s.UserName})"
            }), "Id", "FullName");

            return View(group.Students ?? new List<StudentVm>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttachStudent(int groupId, int studentId)
        {
            var attachVm = new AttachDetachStudentToGroupVm
            {
                GroupId = groupId,
                StudentId = studentId
            };

            _groupService.AttachStudentToGroup(attachVm);

            return RedirectToAction(nameof(ManageGroupStudents), new { id = groupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetachStudent(int groupId, int studentId)
        {
            var detachVm = new AttachDetachStudentToGroupVm
            {
                GroupId = groupId,
                StudentId = studentId
            };

            _groupService.DetachStudentFromGroup(detachVm);

            return RedirectToAction(nameof(ManageGroupStudents), new { id = groupId });
        }
    }
}