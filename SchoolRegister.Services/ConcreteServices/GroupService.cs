using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var groupEntity = Mapper.Map<Group>(addOrUpdateVm);

                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Groups.Add(groupEntity);
                else
                    DbContext.Groups.Update(groupEntity);

                DbContext.SaveChanges();

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");

                var groupEntity = DbContext.Groups.FirstOrDefault(filterExpression);
                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterExpression = null)
        {
            try
            {
                var groupEntities = DbContext.Groups.AsQueryable();

                if (filterExpression != null)
                    groupEntities = groupEntities.Where(filterExpression);

                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
                return groupVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroupsWithSubjects(Expression<Func<Group, bool>> filterExpression = null)
        {
            try
            {
                var groupEntities = DbContext.Groups
                    .Include(g => g.SubjectGroups)
                    .ThenInclude(sg => sg.Subject)
                    .AsQueryable();

                if (filterExpression != null)
                    groupEntities = groupEntities.Where(filterExpression);

                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
                return groupVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
