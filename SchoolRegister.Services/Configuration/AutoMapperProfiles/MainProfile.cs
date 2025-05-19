using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.Linq;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            // SUBJECT <-> SubjectVm
            CreateMap<Subject, SubjectVm>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                    src.Teacher == null ? null : $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(y => y.Group)));

            CreateMap<AddOrUpdateSubjectVm, Subject>();
            CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

            // GROUP <-> GroupVm
            CreateMap<Group, GroupVm>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src =>
                    src.SubjectGroups.Select(s => s.Subject)));

            CreateMap<AddOrUpdateGroupVm, Group>();

            // STUDENT <-> StudentVm
            CreateMap<Student, StudentVm>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src =>
                    src.Group == null ? null : src.Group.Name))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src =>
                    src.Parent == null ? null : $"{src.Parent.FirstName} {src.Parent.LastName}"));

            // NEW: STUDENT -> GradesReportVm
            CreateMap<Student, GradesReportVm>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src =>
                    src.Group != null ? src.Group.Name : null))
                .ForMember(dest => dest.AverageGrade, opt => opt.MapFrom(src =>
                    src.Grades != null && src.Grades.Any()
                        ? (double?)src.Grades.Average(g => (int)g.GradeValue)
                        : null));

            // GRADE <-> GradeVm
            CreateMap<Grade, GradeVm>();
            CreateMap<AddGradeToStudentVm, Grade>();

            // TEACHER -> TeacherVm
            CreateMap<Teacher, TeacherVm>();

            // SUBJECTGROUP <-> AttachDetachSubjectGroupVm
            CreateMap<AttachDetachSubjectGroupVm, SubjectGroup>()
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId));

            // VM to VM
            CreateMap<GetGradesReportVm, GradesReportVm>();
        }
    }
}