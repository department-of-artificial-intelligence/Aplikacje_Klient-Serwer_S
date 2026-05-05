using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<Subject, SubjectVm>()
            .IncludeAllDerived()
            .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => src.Teacher == null ?
                null : $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
            .ForMember(dest => dest.Groups, x => x.MapFrom(src => src.SubjectGroups.Select(y => y.Group)));

        CreateMap<AddOrUpdateSubjectVm, Subject>();

        CreateMap<Group, GroupVm>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students))
            .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.SubjectGroups.Select(s => s.Subject)));

        CreateMap<AddOrUpdateGroupVm, Group>()
            .IncludeAllDerived();

        CreateMap<GroupVm, AddOrUpdateGroupVm>();

        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

        CreateMap<Student, StudentVm>()
            .IncludeAllDerived()
            .ForMember(dest => dest.GroupName, x => x.MapFrom(src => src.Group == null ? null : src.Group.Name))
            .ForMember(dest => dest.ParentName,
                x => x.MapFrom(src => src.Parent == null ? null : $"{src.Parent.FirstName} {src.Parent.LastName}"));

        CreateMap<Teacher, TeacherVm>().IncludeAllDerived();
        CreateMap<Grade, GradeVm>().IncludeAllDerived();

        CreateMap<RegisterNewUserVm, User>()
            .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
            .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Parent>()
            .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
            .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Student>()
            .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
            .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Teacher>()
            .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
            .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Title, y => y.MapFrom(src => src.TeacherTitles));
    }
}