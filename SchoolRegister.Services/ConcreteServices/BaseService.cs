using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;

namespace SchoolRegister.Services.ConcreteServices
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext DbContext = null!;
        protected readonly ILogger Logger = null!;
        protected readonly IMapper Mapper = null!;

        public BaseService(ApplicationDbContext db_context, IMapper mapper, ILogger logger)
        {
            DbContext = db_context;
            Logger = logger;
            Mapper = mapper;
        }
    }
}