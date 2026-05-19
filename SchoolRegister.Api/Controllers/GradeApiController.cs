using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Teacher")]
public class GradeApiController : BaseApiController
{
    private readonly IGradeService _gradeService;
    private readonly UserManager<User> _userManager;

    public GradeApiController(ILogger logger, IMapper mapper, 
        IGradeService gradeService, UserManager<User> userManager) : base(logger, mapper)
    {
        _gradeService = gradeService;
        _userManager = userManager;
    }

    // POST: api/GradeApi/AddGrade
    [HttpPost("AddGrade")]
    public async Task<IActionResult> Post([FromBody] AddGradeToStudentVm addGradeVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Pobranie zalogowanego nauczyciela z kontekstu HTTP (Token JWT)
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is not Teacher teacher)
                return BadRequest("Zalogowany użytkownik nie jest nauczycielem.");

            // Przekazanie do serwisu id nauczyciela i danych oceny
            var result = _gradeService.AddGradeToStudent(addGradeVm);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Wystąpił błąd podczas wystawiania oceny.");
        }
    }
}