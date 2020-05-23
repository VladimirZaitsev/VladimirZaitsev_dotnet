using AutoMapper;
using BLL.Models;
using DAL.Dtos;

namespace BLL.Automapper
{
    public class AutomapperBLLConfig : Profile
    {
        public AutomapperBLLConfig()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<MissedClass, MissedLecturesDto>().ReverseMap();
            CreateMap<Lecturer, LecturerDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
