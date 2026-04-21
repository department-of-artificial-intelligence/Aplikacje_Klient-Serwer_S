using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        private readonly UserManager<User> _userManager;

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                if (addGradeToStudentVm == null)
                {
                    throw new ArgumentNullException($"addGradeToStudentVm is null");
                }
                var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
                gradeEntity.DateOfIssue = DateTime.Now;

                DbContext.Grades.Add(gradeEntity);
                DbContext.SaveChanges();

                var gradeVm = Mapper.Map<GradeVm>(gradeEntity);
                return gradeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        public GradesReportVm GetGradesReportForStudent(GetGradeReportVm getGradesVm)
        {
            try
            {
                if (getGradesVm == null)
                {
                    throw new ArgumentNullException($"getGradesVm is null");
                }

                var studentEntity = Mapper.Map<Student>(getGradesVm);

                var reportVm = Mapper.Map<GradesReportVm>(studentEntity);
                return reportVm;

            }

            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

    }
}