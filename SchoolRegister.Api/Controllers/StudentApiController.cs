using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Admin, Teacher")]
public class StudentApiController : BaseApiController
{
    private readonly IStudentService _studentService;

    public StudentApiController(ILogger<StudentApiController> logger, IMapper mapper, IStudentService studentService) : base(logger, mapper)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_studentService.GetStudents());
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        var student = _studentService.GetStudent(s => s.Id == id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost("AttachToGroup")]
    public IActionResult AttachToGroup([FromBody] AttachDetachStudentToGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = _studentService.AttachStudentToGroup(vm);
        if (!result) return BadRequest("Nie udało się przypisać ucznia do grupy.");
        return Ok(new { success = true });
    }

    [HttpPost("DetachFromGroup")]
    public IActionResult DetachFromGroup([FromBody] AttachDetachStudentToGroupVm vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = _studentService.DetachStudentFromGroup(vm);
        if (!result) return BadRequest("Nie udało się wypisać ucznia z grupy.");
        return Ok(new { success = true });
    }
}