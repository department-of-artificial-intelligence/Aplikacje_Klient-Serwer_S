using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Api.Controllers;

[Authorize(Roles = "Teacher, Admin")]
public class GroupApiController : BaseApiController
{
    private readonly ISubjectService _subjectService;
    private readonly UserManager<User> _userManager;
    public GroupApiController(ILogger logger, IMapper mapper,
    ISubjectService subjectService,
    UserManager<User> userManager) : base(logger, mapper)
    {
        _subjectService = subjectService;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok(_subjectService.GetSubjects());
            else if (await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                if (user is Teacher teacher)
                    return Ok(_subjectService.GetSubjects(x => x.TeacherId == teacher.Id));
                return BadRequest("Teacher is assigned to role, but to the Teacher type.");
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

    
    
    // helper method
    private IActionResult PostOrPutSubject(AddOrUpdateSubjectVm addOrUpdateSubjectVm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var subjectVm = _subjectService.AddOrUpdateSubject(addOrUpdateSubjectVm);
            return Ok(subjectVm);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest("Error occurred");
        }
    }
}
