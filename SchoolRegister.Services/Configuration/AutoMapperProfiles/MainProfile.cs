using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        // Subject -> SubjectVm
        CreateMap<Subject, SubjectVm>()
            .ForMember(dest => dest.TeacherName,
                opt => opt.MapFrom(src =>
                    src.Teacher == null
                        ? null
                        : $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
            .ForMember(dest => dest.Groups,
                opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(x => x.Group)));

        // ViewModel -> Entity
        CreateMap<AddOrUpdateSubjectVm, Subject>();

        // Entity -> ViewModel
        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

        // Group -> GroupVm
        CreateMap<Group, GroupVm>()
            .ForMember(dest => dest.Students,
                opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.Subjects,
                opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(x => x.Subject)));

        // Student -> StudentVm
        CreateMap<Student, StudentVm>()
            .ForMember(dest => dest.GroupName,
                opt => opt.MapFrom(src =>
                    src.Group == null ? null : src.Group.Name))
            .ForMember(dest => dest.ParentName,
                opt => opt.MapFrom(src =>
                    src.Parent == null
                        ? null
                        : $"{src.Parent.FirstName} {src.Parent.LastName}"));
    }
}