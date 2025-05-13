using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private UserManager<User> _userManager;

        public GradeService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            ILogger logger,
            UserManager<User> userManager
        )
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                var teacher = DbContext
                    .Users.OfType<Teacher>()
                    .FirstOrDefault(t => t.Id == addGradeToStudentVm.TeacherId);

                var t = _userManager.IsInRoleAsync(teacher, "Teacher");
                if (!t.Result)
                {
                    throw new InvalidOperationException("user is not a teacher");
                }

                var student = DbContext
                    .Users.OfType<Student>()
                    .FirstOrDefault(t => t.Id == addGradeToStudentVm.StudentId);

                if (student == null)
                {
                    throw new InvalidOperationException(
                        $"no student with id {addGradeToStudentVm.StudentId}"
                    );
                }

                var subject = DbContext
                    .Subjects.OfType<Subject>()
                    .FirstOrDefault(t => t.Id == addGradeToStudentVm.SubjectId);

                if (subject == null)
                {
                    throw new InvalidOperationException(
                        $"no subject with id {addGradeToStudentVm.SubjectId}"
                    );
                }

                if (student.Grades == null)
                {
                    student.Grades = new List<Grade>();
                }

                var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);

                student.Grades.Add(gradeEntity);

                DbContext.SaveChanges();

                var gradeVm = Mapper.Map<GradeVm>(gradeEntity);

                return gradeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            try
            {
                var student = DbContext
                    .Users.OfType<Student>()
                    .FirstOrDefault(t => t.Id == getGradesVm.StudentId);

                var user = DbContext
                    .Users.OfType<User>()
                    .FirstOrDefault(t => t.Id == getGradesVm.GetterUserId);

                var t = _userManager.IsInRoleAsync(user, "Student");
                bool flag = false;

                if (t.Result)    
                {
                    if(getGradesVm.StudentId != getGradesVm.GetterUserId)
                    {
                        throw new InvalidOperationException(
                                $"student can only see his/her own grades");
                    }
                }
                else
                {
                   t = _userManager.IsInRoleAsync(user, "Teacher");
                   if(!t.Result)
                   {
                        t = _userManager.IsInRoleAsync(user, "Parent");
                        if(t.Result)
                        {
                            foreach (var s in (user as Parent).Students)
                            {
                                if(s.ParentId == getGradesVm.GetterUserId)
                                {
                                    flag = true;
                                }
                            }

                            if(flag == false)
                            {
                                throw new InvalidOperationException(
                                    $"user with id ${getGradesVm.GetterUserId} is not parent of student with id ${getGradesVm.StudentId}");
                            }                            
                        }
                        else
                        {
                            throw new InvalidOperationException("user is not a student, parent or teacher");
                        }
                   }
                }


                //int StudentId;
                //int GetterUserId;

                return new GradesReportVm();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
