using AutoMapper;
using BLL.Models;
using DAL.DTO;

namespace BLL.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<MissedLecture, MissedLecturesDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}
