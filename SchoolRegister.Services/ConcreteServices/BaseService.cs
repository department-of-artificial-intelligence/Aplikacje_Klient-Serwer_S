using System;
using System.Collections.Generic;
using System.Linq.Expressions;                
using AutoMapper;                                 
using Microsoft.Extensions.Logging;               
using SchoolRegister.DAL.EF;                      
using SchoolRegister.Model.DataModels;            
using SchoolRegister.Services.Interfaces;         
using SchoolRegister.ViewModels.VM; 

namespace SchoolRegister.Services.ConcreteServices
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        protected BaseService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        {
            DbContext = dbContext;
            Mapper    = mapper;
            Logger    = logger;
        }
    }
}
