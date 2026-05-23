using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;

namespace SchoolRegister.Api.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class GroupApiController : BaseApiController
    {
        private readonly IGroupService _groupService;

        public GroupApiController(ILogger logger, IMapper mapper, IGroupService groupService) : base(logger, mapper)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var groups = _groupService.GetGroups();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

[HttpPost("student")]
        [Authorize(Roles = "Admin, Teacher")]
       
        public IActionResult AddStudentToGroup([FromBody] AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

             
                var result = _groupService.AttachStudentToGroup(attachStudentToGroupVm);
                return Ok(new { success = result != null }); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpDelete("student")]
        [Authorize(Roles = "Admin, Teacher")]
    
        public IActionResult RemoveStudentFromGroup([FromBody] AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

            
                var result = _groupService.DetachStudentFromGroup(detachStudentToGroupVm);
                return Ok(new { success = result != null });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }
    }
}