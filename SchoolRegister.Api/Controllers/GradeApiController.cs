using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[Authorize] // Każdy zalogowany użytkownik ma dostęp, ale konkretne role są sprawdzane wewnątrz
public class GradeApiController : BaseApiController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;

    public GradeApiController(ILogger logger, IMapper mapper,
    IGradeService gradeService,
    UserManager<User> userManager) : base(logger, mapper)
    {
        _gradeService = gradeService;
        _userManager = userManager;
    }

    // Wyświetlanie ocen (raportu) dla zalogowanego użytkownika (Student, Parent, Teacher, Admin)
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null) return Unauthorized();

            // 1. Jeśli to Student - pobiera raport tylko dla siebie
            if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                var reportVm = new GetGradesReportVm { StudentId = user.Id };
                return Ok(_gradeService.GetGradesReportForStudent(reportVm));
            }

            // 2. Jeśli to Parent - pobiera raport dla swoich dzieci
            // (Tutaj w zależności od logiki biznesowej możesz potrzebować przekazać ID dziecka w parametrze, 
            // ale jeśli Parent ma domyślne dziecko, pobieramy raport dla studenta powiązanego z rodzicem)
            else if (await _userManager.IsInRoleAsync(user, "Parent"))
            {
                if (user is Parent parent && parent.Students != null && parent.Students.Any())
                {
                    // Przykład dla pierwszego dziecka z listy (lub pętla/lista raportów w zależności od implementacji)
                    var firstChildId = parent.Students.First().Id;
                    var reportVm = new GetGradesReportVm { StudentId = firstChildId };
                    return Ok(_gradeService.GetGradesReportForStudent(reportVm));
                }
                return BadRequest("Parent has no students assigned.");
            }

            // 3. Jeśli to Admin lub Teacher - mają dostęp do ogólnych danych (np. wymagają przekazania parametrów query)
            else if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                // W przypadku nauczyciela/admina raport zazwyczaj wymaga podania ID studenta w query stringu, np. ?studentId=5
                // Jeśli brak parametru, można zwrócić BadRequest lub pusty raport
                return BadRequest("Admin and Teacher roles must specify a target Student ID using detailed endpoints.");
            }

            else
                return BadRequest("Error occurred");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    // Pobieranie raportu konkretnego studenta po ID (np. gdy Nauczyciel lub Admin przegląda profil ucznia)
    [HttpGet("{studentId:int:min(1)}")]
    [Authorize(Roles = "Teacher, Admin")]
    public async Task<IActionResult> Get(int studentId)
    {
        try
        {
            var reportVm = new GetGradesReportVm { StudentId = studentId };
            var report = _gradeService.GetGradesReportForStudent(reportVm);
            if (report == null)
                return NotFound();

            return Ok(report);
        }
        catch (ArgumentNullException ane)
        {
            Logger.LogError(ane, ane.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }

    // Wystawianie oceny studentowi - Dostępne TYLKO dla roli Teacher
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public IActionResult Post([FromBody] AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gradeVm = _gradeService.AddGradeToStudent(addGradeToStudentVm);
            return Ok(gradeVm);
        }
        catch (ArgumentException ae)
        {
            // Łapie błędy walidacji z serwisu (np. brak studenta lub przedmiotu w bazie)
            Logger.LogError(ae, ae.Message);
            return BadRequest(ae.Message);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }
}