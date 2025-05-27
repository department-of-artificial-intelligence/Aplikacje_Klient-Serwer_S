using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            // Subject  ➟  SubjectVm
            CreateMap<Subject, SubjectVm>()
                .ForMember(dest => dest.TeacherName,
                           m => m.MapFrom(src => src.Teacher == null
                               ? null
                               : $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.Groups,
                           m => m.MapFrom(src => src.SubjectGroups.Select(sg => sg.Group)))
                .ReverseMap(); // umożliwia SubjectVm ➟ Subject

            // AddOrUpdateSubjectVm  ➟  Subject
            CreateMap<AddOrUpdateSubjectVm, Subject>();

            /* ---------- mapy Group ---------- */

            CreateMap<Group, GroupVm>()
                .ForMember(dest => dest.Students,
                           m => m.MapFrom(src => src.Students))
                .ForMember(dest => dest.Subjects,
                           m => m.MapFrom(src => src.SubjectGroups.Select(sg => sg.Subject)));

            // AddOrUpdateGroupVm (stworzysz później) ➟ Group
            // CreateMap<AddOrUpdateGroupVm, Group>();

            /* ---------- mapy Student ---------- */

            CreateMap<Student, StudentVm>()
                .ForMember(dest => dest.GroupName,
                           m => m.MapFrom(src => src.Group == null ? null : src.Group.Name))
                .ForMember(dest => dest.ParentName,
                           m => m.MapFrom(src => src.Parent == null
                               ? null
                               : $"{src.Parent.FirstName} {src.Parent.LastName}"));

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

            CreateMap<RegisterNewUserVm, Parent>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RegisterNewUserVm, Student>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<RegisterNewUserVm, Teacher>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.TeacherTitles));
        }
    }
}
