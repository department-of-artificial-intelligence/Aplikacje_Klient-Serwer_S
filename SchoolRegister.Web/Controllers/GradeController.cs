using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegister.Web.Controllers
{
    [Authorize]
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;

        public GradeController(
            IGradeService gradeService, 
            ISubjectService subjectService, 
            IStudentService studentService, 
            UserManager<User> userManager)
        {
            _gradeService = gradeService;
            _subjectService = subjectService;
            _studentService = studentService;
            _userManager = userManager;
        }


        
        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult AddGrade()
        {
           
            ViewBag.Subjects = new SelectList(_subjectService.GetSubjects(), "Id", "Name");
            
            var students = _studentService.GetStudents().Select(s => new {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}"
            });
            ViewBag.Students = new SelectList(students, "Id", "FullName");
            
            return View(new AddGradeToStudentVm());
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGrade(AddGradeToStudentVm vm)
        {
            if (ModelState.IsValid)
            {
          
                _gradeService.AddGradeToStudent(vm);
                TempData["Success"] = "Grade added successfully!";
                return RedirectToAction(nameof(AddGrade));
            }

 
            ViewBag.Subjects = new SelectList(_subjectService.GetSubjects(), "Id", "Name", vm.SubjectId);
            var students = _studentService.GetStudents().Select(s => new {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}"
            });
            ViewBag.Students = new SelectList(students, "Id", "FullName", vm.StudentId);
            
            return View(vm);
        }

        
        
        [Authorize(Roles = "Student, Parent")]
        [HttpGet]
        public async Task<IActionResult> Report(int? studentId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var vm = new GetGradesReportVm
            {
                GetterUserId = user.Id,
                
                StudentId = studentId ?? user.Id 
            };

            var report = _gradeService.GetGradesReportForStudent(vm);
            return View(report);
        }
    }
}