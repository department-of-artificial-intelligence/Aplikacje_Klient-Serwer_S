using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Student, StudentVm>();
            CreateMap<Teacher, TeacherVm>();
            CreateMap<Group, GroupVm>();
            CreateMap<Subject, SubjectVm>();
            CreateMap<Grade, GradeVm>();
        }
    }
}