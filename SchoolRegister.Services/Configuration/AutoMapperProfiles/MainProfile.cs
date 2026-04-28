using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<Subject, SubjectVm>()
            .ForMember(dest => dest.TeacherName,
                opt => opt.MapFrom(src =>
                    src.Teacher == null
                        ? null
                        : $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
            .ForMember(dest => dest.Groups,
                opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(x => x.Group)))
            .ForMember(dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId));

        CreateMap<AddOrUpdateSubjectVm, Subject>();
        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

        CreateMap<Group, GroupVm>()
            .ForMember(dest => dest.Students,
                opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.Subjects,
                opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(x => x.Subject)));

        CreateMap<Student, StudentVm>()
            .ForMember(dest => dest.GroupName,
                opt => opt.MapFrom(src =>
                    src.Group == null ? null : src.Group.Name))
            .ForMember(dest => dest.ParentName,
                opt => opt.MapFrom(src =>
                    src.Parent == null
                        ? null
                        : $"{src.Parent.FirstName} {src.Parent.LastName}"));

        CreateMap<Teacher, TeacherVm>()
            .ForMember(dest => dest.Subjects,
                opt => opt.MapFrom(src => src.Subjects));

        CreateMap<Grade, GradeVm>();

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