using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(ApplicationDbContext db_context, IMapper mapper, ILogger logger)
            : base(db_context, mapper, logger) { }

        public SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm add_or_update_vm)
        {
            try
            {
                if (add_or_update_vm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var subject_entity = Mapper.Map<Subject>(add_or_update_vm);

                if (!add_or_update_vm.Id.HasValue || add_or_update_vm.Id == 0)
                    DbContext.Subjects.Add(subject_entity);
                else
                    DbContext.Subjects.Update(subject_entity);

                DbContext.SaveChanges();

                var subject_vm = Mapper.Map<SubjectVm>(subject_entity);
                return subject_vm;
            }
            catch (Exception ex_obj)
            {
                Logger.LogError(ex_obj, ex_obj.Message);
                throw;
            }
        }

        public SubjectVm GetSubject(Expression<Func<Subject, bool>> filter_expression)
        {
            try
            {
                if (filter_expression == null)
                    throw new ArgumentNullException($"FilterExpression is null");

                var subject_entity = DbContext.Subjects.FirstOrDefault(filter_expression);
                var subject_vm = Mapper.Map<SubjectVm>(subject_entity);

                return subject_vm;
            }
            catch (Exception ex_obj)
            {
                Logger.LogError(ex_obj, ex_obj.Message);
                throw;
            }
        }

        public IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filter_expression = null)
        {
            try
            {
                var subject_entities = DbContext.Subjects.AsQueryable();

                if (filter_expression != null)
                    subject_entities = subject_entities.Where(filter_expression);

                var subject_vms = Mapper.Map<IEnumerable<SubjectVm>>(subject_entities);
                return subject_vms;
            }
            catch (Exception ex_obj)
            {
                Logger.LogError(ex_obj, ex_obj.Message);
                throw;
            }
        }
    }
}