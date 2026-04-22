using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        //AutoMapper maps
        CreateMap<Subject, SubjectVm>() // map from Subject(src) to SubjectVm(dst)
                                        // custom mapping: FirstName and LastName concat string to TeacherName
        .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => src.Teacher == null ?
        null :
        $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
        // custom mapping: IList<Group> to IList<GroupVm>
        .ForMember(dest => dest.Groups, x => x.MapFrom(src => src.SubjectGroups.Select(y => y.Group)));
        CreateMap<AddOrUpdateSubjectVm, Subject>();
        CreateMap<Group, GroupVm>()
        .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students))
        .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.SubjectGroups.Select(s => s.Subject)));
        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();
        CreateMap<Student, StudentVm>()
        .ForMember(dest => dest.GroupName, x => x.MapFrom(src => src.Group == null ? null : src.Group.Name))
        .ForMember(dest => dest.ParentName,
        x => x.MapFrom(src => src.Parent == null ? null : $"{src.Parent.FirstName} {src.Parent.LastName}"));
        //....... other maps.........
        CreateMap<Teacher, TeacherVm>();
        CreateMap<Grade, GradeVm>()
        .ForMember(dest => dest.GradeValue, x => x.MapFrom(src => (double)src.GradeValue))
        .ForMember(dest => dest.SubjectName, x => x.MapFrom(src => src.Subject != null ? src.Subject.Name : string.Empty ));

        CreateMap<AddGradeToStudentVm, Grade>();
        CreateMap<Student, GradesReportVm>()
        .ForMember(dest => dest.StudentName, x => x
            .MapFrom(src => $"{src.FirstName} {src.LastName}"))
        .ForMember(dest => dest.GroupName, x => x.MapFrom(src => src.Group == null ? null : src.Group.Name));

        CreateMap<AddOrUpdateGroupVm, Group>();

    }
}
