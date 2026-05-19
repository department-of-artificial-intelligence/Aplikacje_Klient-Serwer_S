using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers
{
    [Authorize(Roles = "Teacher,Admin")]
    public class GroupApiController : BaseApiController
    {
        private readonly IGroupService groupService;

        public GroupApiController(
            ILogger<GroupApiController> logger,
            IMapper mapper,
            IGroupService groupService) : base(logger, mapper)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var groups = groupService.GetGroups();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            try
            {
                var group = groupService.GetGroup(g => g.Id == id);

                if (group == null)
                    return NotFound();

                return Ok(group);
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            return PostOrPutGroup(addOrUpdateGroupVm);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put([FromBody] AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            return PostOrPutGroup(addOrUpdateGroupVm);
        }

        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]


        [HttpPost("attach-student")]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachStudentToGroup([FromBody] AttachDetachStudentToGroupVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = groupService.AttachStudentToGroup(vm);
                return Ok(student);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("detach-student")]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachStudentFromGroup([FromBody] AttachDetachStudentToGroupVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = groupService.DetachStudentFromGroup(vm);
                return Ok(student);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("attach-subject")]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachSubjectToGroup([FromBody] AttachDetachSubjectGroupVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var group = groupService.AttachSubjectToGroup(vm);
                return Ok(group);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("detach-subject")]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachSubjectFromGroup([FromBody] AttachDetachSubjectGroupVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var group = groupService.DetachSubjectFromGroup(vm);
                return Ok(group);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("attach-teacher")]
        [Authorize(Roles = "Admin")]
        public IActionResult AttachTeacherToSubject([FromBody] AttachDetachSubjectToTeacherVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subject = groupService.AttachTeacherToSubject(vm);
                return Ok(subject);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("detach-teacher")]
        [Authorize(Roles = "Admin")]
        public IActionResult DetachTeacherFromSubject([FromBody] AttachDetachSubjectToTeacherVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subject = groupService.DetachTeacherFromSubject(vm);
                return Ok(subject);
            }
            catch (ArgumentNullException ane)
            {
                Logger.LogError(ane, ane.Message);
                return NotFound();
            }
            catch (ArgumentException ae)
            {
                Logger.LogError(ae, ae.Message);
                return BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest("Error occurred");
            }
        }

        private IActionResult PostOrPutGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var group = groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
                return Ok(group);
            }
            catch (ArgumentException ae)
            {
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
}