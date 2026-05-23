using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Threading.Tasks;

namespace SchoolRegister.Api.Controllers
{
    [Authorize]
    public class GradeApiController : BaseApiController
    {
        private readonly IGradeService _gradeService;
        private readonly UserManager<User> _userManager;

        public GradeApiController(ILogger logger, IMapper mapper, IGradeService gradeService, UserManager<User> userManager) : base(logger, mapper)
        {
            _gradeService = gradeService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Post([FromBody] AddGradeToStudentVm addGradeVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _gradeService.AddGradeToStudent(addGradeVm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

[HttpGet]
[Authorize(Roles = "Student, Parent")]
public async Task<IActionResult> Get()
{
    try
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        
        if (user == null)
            return BadRequest("User not found");

        if (await _userManager.IsInRoleAsync(user, "Student"))
        {
            // TWORZYMY OBIEKT GetGradesReportVm ZAMIAST PRZEKAZYWAĆ SAM INT
            var reportVm = new GetGradesReportVm { StudentId = user.Id };
            var studentGrades = _gradeService.GetGradesReportForStudent(reportVm);
            return Ok(studentGrades);
        }
        
        if (await _userManager.IsInRoleAsync(user, "Parent"))
        {
            // TWORZYMY OBIEKT GetGradesReportVm (dla rodzica)
            // Uwaga: Zależnie od tego, jak wygląda Twoja klasa GetGradesReportVm,
            // właściwość może nazywać się np. ParentId zamiast StudentId.
            var reportVm = new GetGradesReportVm { StudentId = user.Id }; // lub ParentId = user.Id
            var parentGrades = _gradeService.GetGradesReportForStudent(reportVm);
            return Ok(parentGrades);
        }

        return BadRequest("Error occurred");
    }
    catch (Exception ex)
    {
        Logger.LogError(ex, ex.Message);
        return BadRequest("Error occurred");
    }
}
        }
    }
