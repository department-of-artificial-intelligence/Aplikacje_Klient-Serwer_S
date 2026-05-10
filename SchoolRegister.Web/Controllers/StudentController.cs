using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Web.Controllers
{
    [Authorize] // Dostęp tylko dla zalogowanych
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Pobieramy wszystkich uczniów z bazy
            var students = _studentService.GetStudents();
            return View(students);
        }
    }
}