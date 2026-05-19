// Controllers/GradeApiController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize]
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

    
    [HttpGet("[action]")]
    [Authorize(Roles = "Admin, Teacher, Student, Parent")]
    public async Task<IActionResult> GetGradesReport([FromQuery] GetGradesReportVm getGradesReportVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (await _userManager.IsInRoleAsync(user, "Admin") ||
                await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                // Admin i Teacher mogą pobrać raport dla dowolnego studenta
                var report = _gradeService.GetGradesReportForStudent(getGradesReportVm);
                if (report == null)
                    return NotFound();
                return Ok(report);
            }

            if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                if (user is not Student student)
                    return BadRequest("Student is assigned to role, but not to the Student type.");

                // Student może pobrać tylko swój raport
                if (getGradesReportVm.StudentId != student.Id)
                    return Forbid();

                var report = _gradeService.GetGradesReportForStudent(getGradesReportVm);
                if (report == null)
                    return NotFound();
                return Ok(report);
            }

            if (await _userManager.IsInRoleAsync(user, "Parent"))
            {
                if (user is not Parent parent)
                    return BadRequest("Parent is assigned to role, but not to the Parent type.");

                // Parent może pobrać raport tylko dla swoich dzieci
                var childIds = parent.Students.Select(s => s.Id).ToList();
                if (!childIds.Contains(getGradesReportVm.StudentId))
                    return Forbid();

                var report = _gradeService.GetGradesReportForStudent(getGradesReportVm);
                if (report == null)
                    return NotFound();
                return Ok(report);
            }

            return BadRequest("Error occurred");
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

    // POST api/GradeApi/AddGrade
    // Tylko Teacher może wystawiać oceny
    [HttpPost("[action]")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> AddGrade([FromBody] AddGradeToStudentVm addGradeToStudentVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is not Teacher teacher)
                return BadRequest("User is assigned to Teacher role, but not to the Teacher type.");

            // TeacherId ustawiamy z tożsamości zalogowanego użytkownika
            // — nie ufamy wartości przesłanej przez klienta
            addGradeToStudentVm.TeacherId = teacher.Id;

            var gradeVm = _gradeService.AddGradeToStudent(addGradeToStudentVm);
            return Ok(gradeVm);
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
}