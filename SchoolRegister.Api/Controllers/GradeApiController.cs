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
    private readonly IGradeService grade_service;
    private readonly UserManager<User> user_manager;

    public GradeApiController(ILogger<GradeApiController> logger, IMapper mapper,
                              IGradeService grade_service_param,
                              UserManager<User> user_manager_param) : base(logger, mapper)
    {
        grade_service = grade_service_param;
        user_manager = user_manager_param;
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public IActionResult Post([FromBody] AddGradeToStudentVm add_grade_vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result_grade = grade_service.AddGradeToStudent(add_grade_vm);
        return Ok(result_grade);
    }

    [HttpGet]
    [Authorize(Roles = "Student, Parent")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var current_user = await user_manager.FindByNameAsync(User.Identity?.Name);

            if (await user_manager.IsInRoleAsync(current_user, "Student"))
            {
                var report_vm = new GetGradesReportVm { StudentId = current_user.Id };
                var student_grades = grade_service.GetGradesReportForStudent(report_vm);

                return Ok(student_grades);
            }
            else if (await user_manager.IsInRoleAsync(current_user, "Parent"))
            {
                // Dla rodzica przygotowujemy odpowiedni parametr wyszukiwania. 
                // Upewnij się, jakie pola wyszukiwania posiada klasa GetGradesReportVm
                var report_vm = new GetGradesReportVm { StudentId = current_user.Id };
                var parent_grades = grade_service.GetGradesReportForStudent(report_vm);

                return Ok(parent_grades);
            }

            return BadRequest("Brak uprawnień do przeglądania ocen.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Wystąpił błąd");
        }
    }
}