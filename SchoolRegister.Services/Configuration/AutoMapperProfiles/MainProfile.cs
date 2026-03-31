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
            CreateMap<Teacher, TeacherVm>();
            CreateMap<AddOrUpdateGroupVm, Group>();
            CreateMap<Grade, GradeVm>();
            CreateMap<AddGradeToStudentVm, Grade>();
            CreateMap<Subject, SubjectVm>()
                .ForMember(dest_obj => dest_obj.TeacherName, opt_obj => opt_obj.MapFrom(source_obj =>
                    source_obj.Teacher == null ? null : $"{source_obj.Teacher.FirstName} {source_obj.Teacher.LastName}"))
                .ForMember(dest_obj => dest_obj.Groups, opt_obj => opt_obj.MapFrom(source_obj =>
                    source_obj.SubjectGroups.Select(subject_group => subject_group.Group)));

            CreateMap<AddOrUpdateSubjectVm, Subject>();

            CreateMap<Group, GroupVm>()
                .ForMember(dest_obj => dest_obj.Students, opt_obj => opt_obj.MapFrom(source_obj => source_obj.Students))
                .ForMember(dest_obj => dest_obj.Subjects, opt_obj => opt_obj.MapFrom(source_obj =>
                    source_obj.SubjectGroups.Select(subject_group => subject_group.Subject)));

            CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

            CreateMap<Student, StudentVm>()
                .ForMember(dest_obj => dest_obj.GroupName, opt_obj => opt_obj.MapFrom(source_obj =>
                    source_obj.Group == null ? null : source_obj.Group.Name))
                .ForMember(dest_obj => dest_obj.ParentName, opt_obj => opt_obj.MapFrom(source_obj =>
                    source_obj.Parent == null ? null : $"{source_obj.Parent.FirstName} {source_obj.Parent.LastName}"));
        }
    }
}