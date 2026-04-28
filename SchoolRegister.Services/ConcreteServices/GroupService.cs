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
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
            : base(dbContext, mapper, logger) {}

        public GroupVm GetGroup(Expression<Func<Group, bool>> filter)
        {
            try
            {
                var entity = DbContext.Groups.FirstOrDefault(filter);

                if (entity == null)
                    return null;

                return Mapper.Map<GroupVm>(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>>? filter = null)
        {
            try
            {
                var entities = DbContext.Groups.AsQueryable();

                if (filter != null)
                    entities = entities.Where(filter);

                return Mapper.Map<IEnumerable<GroupVm>>(entities);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}