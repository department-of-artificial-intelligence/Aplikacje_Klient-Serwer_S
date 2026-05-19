using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Api.Controllers
{
    [Authorize(Roles = "Teacher,Parent,Student,Admin")]
    public class GradeApiController : BaseApiController
    {
        private readonly IGradeService gradeService;

        public GradeApiController(
            ILogger<GradeApiController> logger,
            IMapper mapper,
            IGradeService gradeService) : base(logger, mapper)
        {
            this.gradeService = gradeService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public IActionResult Post([FromBody] AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var gradeVm = gradeService.AddGradeToStudent(addGradeToStudentVm);
                return Ok(gradeVm);
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

        [HttpPost("report")]
        public IActionResult GetGradesReport([FromBody] GetGradesReportVm getGradesReportVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var report = gradeService.GetGradesReportForStudent(getGradesReportVm);

                if (report == null)
                    return NotFound();

                return Ok(report);
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
    }
}